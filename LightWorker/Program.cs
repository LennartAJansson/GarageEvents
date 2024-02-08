using GarageEvents.Extensions;
using GarageEvents.Nats.Extensions;

using LightWorker;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);

builder.Services.AddHostedService<LightWorker>();

IHost host = builder.Build();
host.Run();
