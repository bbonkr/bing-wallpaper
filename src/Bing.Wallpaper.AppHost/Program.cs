using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Bing_Wallpaper>("bing-wallpaper");

builder.Build().Run();
