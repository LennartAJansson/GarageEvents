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
      .AddTransient<IGarageHandler, GarageHandler>()
      .AddTransient<IDoorHandler, DoorHandler>()
      .AddTransient<ILightHandler, LightHandler>();
    return services;
  }
}
