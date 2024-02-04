using GarageEvents.Extensions;
using GarageEvents.Nats.Extensions;
using GarageEvents.Remote;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);
IHost host = builder.Build();

using IServiceScope scope = host.Services.CreateScope();
IRemote remote = scope.ServiceProvider.GetRequiredService<IRemote>();

await remote.OpenDoor();
await remote.LightsOn();
await remote.LightsOff();
await remote.CloseDoor();

Console.ReadKey();
