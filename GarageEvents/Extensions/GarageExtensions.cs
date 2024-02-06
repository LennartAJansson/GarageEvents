namespace GarageEvents.Extensions;

using GarageEvents.Door;
using GarageEvents.Garage;
using GarageEvents.Light;
using GarageEvents.Remote;

using Microsoft.Extensions.DependencyInjection;

public static class GarageExtensions
{
  public static IServiceCollection AddGarageComponents(this IServiceCollection services)
  {
    _ = services
      .AddSingleton<IRemote, DefaultRemote>()
      .AddScoped<IGarageHandler, GarageHandler>()
      .AddScoped<IDoorHandler, DoorHandler>()
      .AddScoped<ILightHandler, LightHandler>();
    return services;
  }
}
