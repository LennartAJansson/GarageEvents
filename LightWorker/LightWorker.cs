namespace LightWorker;

using GarageEvents.Light;
using GarageEvents.Remote;
using GarageEvents.Types;

public class LightWorker(ILogger<LightWorker> logger, ILightHandler handler, IRemote remote)
  : BackgroundService
{
  public override Task StartAsync(CancellationToken cancellationToken)
  {
    _ = handler.StartListen(Listener);
    return base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    handler.StopListen();
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      if (logger.IsEnabled(LogLevel.Information))
      {
        logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
      }
      await Task.Delay(60000, stoppingToken);
    }
  }

  private void Listener(object sender, RemoteActionMessage action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.LightsOnCmd:
        logger.LogInformation("{time:G}: LightWorker is doing work for turning on lights", action.Time);
        //TODO: Implement the light turning on
        _ = Task.Run(remote.LightIsOn);
        break;
      case RemoteActionType.LightsOffCmd:
        logger.LogInformation("{time:G}: LightWorker is doing work for turning off lights", action.Time);
        //TODO: Implement the light turning off
        _ = Task.Run(remote.LightIsOff);
        break;
    }
  }
}
