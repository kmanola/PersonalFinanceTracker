var builder = DistributedApplication.CreateBuilder(args);

//var env = builder.AddAzureAppServiceEnvironment("my-environment");

//TODO var ska jag lägga endpoint? kanske ingensans behövs, annars
// https://learn.microsoft.com/en-us/azure/postgresql/flexible-server/connect-csharp
var endpoint = builder.AddParameter("postgres-endpoint", secret: true);
var userName = builder.AddParameter("postgres-username", secret: true);
var password = builder.AddParameter("postgres-password", secret: true);

builder.AddAzureProvisioning();

var postgres = builder.AddAzurePostgresFlexibleServer("postgres")
    .WithPasswordAuthentication(userName, password);

var postgresdb = postgres.AddDatabase("postgresdb");

postgres.RunAsContainer(c => c.WithPgAdmin());

var api = builder.AddProject<Projects.PersonalFinanceTracker_Api>("personalfinancetracker-api")
       .WithReference(postgresdb);

var migrations = builder.AddEfMigrate(api, postgresdb);

api.WaitForCompletion(migrations);
api.WithChildRelationship(migrations);

builder.Build().Run();
