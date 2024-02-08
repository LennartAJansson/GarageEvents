using GarageEvents.Extensions;
using GarageEvents.Nats.Extensions;

using GarageWorker;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);

builder.Services.AddHostedService<GarageWorker>();

IHost host = builder.Build();
host.Run();
