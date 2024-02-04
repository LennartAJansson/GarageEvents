using GarageEvents.Extensions;
using GarageEvents.Garage;
using GarageEvents.Nats.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);
IHost host = builder.Build();

using IServiceScope scope = host.Services.CreateScope();

using IGarage garage = scope.ServiceProvider.GetRequiredService<IGarage>();

Console.ReadKey();
