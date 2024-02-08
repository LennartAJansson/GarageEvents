namespace GarageWorker;

using GarageEvents.Garage;
using GarageEvents.Types;

public class GarageWorker(ILogger<GarageWorker> logger, IGarageHandler handler)
  : BackgroundService
{
  public bool DoorIsOpen { get; set; } = false;
  public bool LightsAreOn { get; set; } = false;

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
        logger.LogInformation("GarageWorker running at: {time}", DateTimeOffset.Now);
      }
      await Task.Delay(60000, stoppingToken);
    }
  }

  private void Listener(object sender, RemoteActionMessage action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.DoorIsOpen:
        logger.LogInformation("{time:G}: Door is open to the garage", action.Time);
        DoorIsOpen = true;
        break;
      case RemoteActionType.DoorIsClosed:
        logger.LogInformation("{time:G}: Door is closed to the garage", action.Time);
        DoorIsOpen = false;
        break;
      case RemoteActionType.LightIsOn:
        logger.LogInformation("{time:G}: Light is on in the garage", action.Time);
        LightsAreOn = true;
        break;
      case RemoteActionType.LightIsOff:
        logger.LogInformation("{time:G}: Light is off in the garage", action.Time);
        LightsAreOn = false;
        break;
    }
  }
}
