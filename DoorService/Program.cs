using GarageEvents.Door;
using GarageEvents.Extensions;
using GarageEvents.Nats.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);

IHost host = builder.Build();

using IServiceScope scope = host.Services.CreateScope();

IDoorHandler handler = scope.ServiceProvider.GetRequiredService<IDoorHandler>();
handler.StartListen(null);

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

handler.StopListen();
