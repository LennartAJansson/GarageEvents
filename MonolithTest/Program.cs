using GarageEvents.Door;
using GarageEvents.Extensions;
using GarageEvents.Garage;
using GarageEvents.Light;
using GarageEvents.Messages;
using GarageEvents.Nats.Extensions;
using GarageEvents.Remote;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.Services
  .AddGarageComponents()
//TODO: Uncomment to run external events through NATS server
.AddNatsRemote(builder.Configuration);
IHost host = builder.Build();

using IServiceScope scope = host.Services.CreateScope();

IRemote remote = scope.ServiceProvider
  .GetRequiredService<IRemote>();

IDoorHandler door = (IDoorHandler)scope.ServiceProvider
  .GetRequiredService<IDoorHandler>()
  .StartListen(Listener);

ILightHandler light = (ILightHandler)scope.ServiceProvider
  .GetRequiredService<ILightHandler>()
  .StartListen(Listener);

IGarageHandler garage = (IGarageHandler)scope.ServiceProvider
  .GetRequiredService<IGarageHandler>()
  .StartListen(Listener);

await remote.OpenDoor();
while (!garage.DoorIsOpen)
{
  await Task.Delay(1000);
}

await remote.LightsOn();
while (!garage.LightsAreOn)
{
  await Task.Delay(1000);
}

await remote.LightsOff();
while (garage.LightsAreOn)
{
  await Task.Delay(1000);
}

await remote.CloseDoor();
while (garage.DoorIsOpen)
{
  await Task.Delay(1000);
}

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

door.StopListen();
light.StopListen();
garage.StopListen();

//TODO: Add a listener for the remote event
static void Listener(object sender, RemoteAction action) => Console.WriteLine();