using GarageEvents.Extensions;
using GarageEvents.Garage;
using GarageEvents.Nats.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration)
  ;
IHost host = builder.Build();

using IServiceScope scope = host.Services.CreateScope();

IGarageHandler garage = scope.ServiceProvider
  .GetRequiredService<IGarageHandler>()
  .StartListen(null);

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

garage.StopListen();
