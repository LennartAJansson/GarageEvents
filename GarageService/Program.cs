using GarageEvents.Extensions;
using GarageEvents.Garage;
using GarageEvents.Messages;
using GarageEvents.Nats.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);
IHost host = builder.Build();

using IServiceScope scope = host.Services.CreateScope();

IGarageHandler garage = (IGarageHandler)scope.ServiceProvider
  .GetRequiredService<IGarageHandler>()
  .StartListen(Listener);

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

garage.StopListen();

//TODO: Add a listener for the remote event
static void Listener(object sender, RemoteAction action) => Console.WriteLine();