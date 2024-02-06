﻿using GarageEvents.Door;
using GarageEvents.Extensions;
using GarageEvents.Messages;
using GarageEvents.Nats.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);
IHost host = builder.Build();

using IServiceScope scope = host.Services.CreateScope();

IDoorHandler handler = scope.ServiceProvider.GetRequiredService<IDoorHandler>();
handler.StartListen(Listener);


Console.WriteLine("Press any key to continue...");
Console.ReadKey();

handler.StopListen();

//TODO: Add a listener for the remote event
static void Listener(object sender, RemoteAction action) => Console.WriteLine();