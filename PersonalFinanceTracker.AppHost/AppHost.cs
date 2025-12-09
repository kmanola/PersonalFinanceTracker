using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var postgresdb = postgres.AddDatabase("postgresdb");

builder.AddProject<Projects.PersonalFinanceTracker_Api>("personalfinancetracker-api")
       .WithReference(postgresdb);


builder.Build().Run();
