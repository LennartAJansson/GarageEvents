using GarageEvents.Door;
using GarageEvents.Extensions;
using GarageEvents.Garage;
using GarageEvents.Light;
using GarageEvents.Nats.Extensions;
using GarageEvents.Remote;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.Services
  .AddGarageComponents().AddNatsRemote(builder.Configuration);
IHost host = builder.Build();

using IServiceScope scope = host.Services.CreateScope();

IRemote remote = scope.ServiceProvider.GetRequiredService<IRemote>();

IDoorHandler door = scope.ServiceProvider.GetRequiredService<IDoorHandler>().StartListen();

ILightHandler light = scope.ServiceProvider.GetRequiredService<ILightHandler>().StartListen();

IGarageHandler garage = scope.ServiceProvider.GetRequiredService<IGarageHandler>().StartListen();

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
