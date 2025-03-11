namespace GarageEvents.Nats.Extensions;

using System;
using System.Linq;

using GarageEvents.Nats.Configuration;
using GarageEvents.Nats.Remote;
using GarageEvents.Remote;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NATS.Client.Hosting;

public static class NatsExtensions
{
  public static IServiceCollection AddNatsRemote(this IServiceCollection services, IConfiguration configuration)
  {
    NatsServiceConfig? config = configuration.GetSection("Nats").Get<NatsServiceConfig>()
      ?? throw new ArgumentException("NATS config not found");
    _ = services.AddSingleton(config);
    
    _ = services.Remove(services.First(x => x.ServiceType == typeof(IRemote)));
    _ = services.AddSingleton<IRemote, NatsRemote>();
    
    _ = services.AddNats();
    
    return services;
  }
}
