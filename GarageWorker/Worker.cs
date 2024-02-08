namespace GarageWorker;

using GarageEvents.Garage;
using GarageEvents.Types;

public class Worker(ILogger<Worker> logger, IGarageHandler handler) : BackgroundService
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
      await Task.Delay(30000, stoppingToken);
    }
  }

  //TODO: Add a listener for the remote event
  private void Listener(object sender, RemoteActionMessage action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.LightsOnCmd:
        logger.LogInformation("{time:G}: GarageWorker is doing work for turning on lights", action.Time);
        break;
      case RemoteActionType.LightsOffCmd:
        logger.LogInformation("{time:G}: GarageWorker is doing work for turning off lights", action.Time);
        break;
      case RemoteActionType.OpenDoorCmd:
        logger.LogInformation("{time:G}: GarageWorker is doing work for opening the door", action.Time);
        break;
      case RemoteActionType.CloseDoorCmd:
        logger.LogInformation("{time:G}: GarageWorker is doing work for closing the door", action.Time);
        break;
    }
  }
}
