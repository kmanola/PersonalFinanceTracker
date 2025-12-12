using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

//TODO var ska jag lägga endpoint? kanske ingensans behövs, annars
// https://learn.microsoft.com/en-us/azure/postgresql/flexible-server/connect-csharp
var endpoint = builder.AddParameter("postgres-endpoint", secret: true);
var userName = builder.AddParameter("postgres-username", secret: true);
var password = builder.AddParameter("postgres-password", secret: true);

var postgres = builder.AddAzurePostgresFlexibleServer("postgres")
    .RunAsContainer()
    .WithPasswordAuthentication(userName, password);

var postgresdb = postgres.AddDatabase("postgresdb");

builder.AddProject<Projects.PersonalFinanceTracker_Api>("personalfinancetracker-api")
       .WithReference(postgresdb)
       .WaitFor(postgresdb);


builder.Build().Run();
