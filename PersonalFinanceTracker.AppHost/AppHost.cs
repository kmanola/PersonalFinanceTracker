var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PersonalFinanceTracker_Api>("personalfinancetracker-api");

builder.Build().Run();
