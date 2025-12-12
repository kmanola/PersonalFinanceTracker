using Microsoft.EntityFrameworkCore;
using Modules.Finance.Features.Shared.Contracts;
using Modules.Finance.Features.Shared.Services;
using Modules.Finance.Infrastructure.Data;

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

app.Run();
