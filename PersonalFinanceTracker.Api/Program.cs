using Microsoft.EntityFrameworkCore;
using Modules.Finance.Features.Shared.Contracts;
using Modules.Finance.Features.Shared.Services;
using Modules.Finance.Infrastructure.Data;
using PersonalFinanceTracker.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<FinanceDbContext>("postgresdb");

builder.Services.AddScoped<ICsvParserService, CsvParserService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Apply migrations BEFORE setting up routes
await app.ApplyMigrationsAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map minimal API endpoints
app.MapGet("/", () => "Hello World!");

// Add a migration status endpoint to verify
app.MapGet("/migrations/status", async (FinanceDbContext db) =>
{
    var appliedMigrations = await db.Database.GetAppliedMigrationsAsync();
    var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
    
    return Results.Ok(new
    {
        Applied = appliedMigrations.ToList(),
        Pending = pendingMigrations.ToList(),
        CanConnect = await db.Database.CanConnectAsync()
    });
});

app.Run();
