﻿using GarageEvents.Extensions;
using GarageEvents.Light;
using GarageEvents.Nats.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);

IHost host = builder.Build();

using IServiceScope scope = host.Services.CreateScope();

ILightHandler handler = scope.ServiceProvider
  .GetRequiredService<ILightHandler>()
  .StartListen(null);

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

handler.StopListen();
