using System.Text;
using Microsoft.EntityFrameworkCore;
using Modules.Finance.Features.Shared.Contracts;
using Modules.Finance.Features.Shared.Services;
using Modules.Finance.Infrastructure.Data;
using PersonalFinanceTracker.Api.Extensions;

// Register the code pages encoding provider for Windows-1252 support
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    var connectionString = builder.Configuration.GetConnectionString("postgresdb");
    builder.Services.AddDbContext<FinanceDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    builder.AddServiceDefaults();
    builder.AddNpgsqlDbContext<FinanceDbContext>("postgresdb");
}

builder.Services.AddScoped<ICsvParserService, CsvParserService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.MapDefaultEndpoints();
}

try
{
    await app.ApplyMigrationsAsync();
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Failed to apply migrations. Application will continue but database may not be ready.");
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.MapPost("/upload", async (IFormFile file, ICsvParserService csvParserService) =>
{
    if (file == null || file.Length == 0)
    {
        return Results.BadRequest("No file uploaded.");
    }

    var tempFilePath = Path.GetTempFileName();
    await using (var stream = File.Create(tempFilePath))
    {
        await file.CopyToAsync(stream);
    }

    var result = await csvParserService.ParseTransactionsAsync(tempFilePath);

    return Results.Ok(new { FilePath = tempFilePath, ParsedData = result });
})
.DisableAntiforgery();

app.MapGet("/migrations/status", async (FinanceDbContext db) =>
{
    try
    {
        var appliedMigrations = await db.Database.GetAppliedMigrationsAsync();
        var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
        var canConnect = await db.Database.CanConnectAsync();

        return Results.Ok(new
        {
            Applied = appliedMigrations.ToList(),
            Pending = pendingMigrations.ToList(),
            CanConnect = canConnect
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new
        {
            Error = ex.Message,
            StackTrace = ex.StackTrace
        });
    }
});

app.Run();
