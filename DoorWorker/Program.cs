using DoorWorker;

using GarageEvents.Extensions;
using GarageEvents.Nats.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);

builder.Services.AddHostedService<DoorWorker>();

IHost host = builder.Build();
host.Run();
