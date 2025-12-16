using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var userName = builder.AddParameter("postgres-username", secret: true);
var password = builder.AddParameter("postgres-password", secret: true);

var postgres = builder.AddPostgres("postgres", userName, password);

var postgresdb = postgres.AddDatabase("postgresdb");

postgres.WithPgAdmin();

var api = builder.AddProject<Projects.PersonalFinanceTracker_Api>("personalfinancetracker-api")
       .WithReference(postgresdb)
       .WithExternalHttpEndpoints()
       .WithUrlForEndpoint("https", url =>
       {
           url.DisplayText = "Swagger UI";
           url.Url = "/swagger";
       })
       .WithUrlForEndpoint("http", url =>
       {
           url.DisplayText = "Swagger UI (HTTP)";
           url.Url = "/swagger";
       });

if (builder.ExecutionContext.IsRunMode)
{
    var migrations = builder.AddEfMigrate(api, postgresdb);
    api.WaitForCompletion(migrations);
    api.WithChildRelationship(migrations);
}

builder.Build().Run();
