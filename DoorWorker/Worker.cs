namespace DoorWorker;

using GarageEvents.Door;
using GarageEvents.Messages;
using GarageEvents.Types;

public class Worker(ILogger<Worker> logger, IDoorHandler handler) : BackgroundService
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
  private void Listener(object sender, RemoteAction action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.OpenDoor:
        logger.LogInformation("{time:G}: DoorWorker is doing additional work for opening the door", action.Time);
        break;
      case RemoteActionType.CloseDoor:
        logger.LogInformation("{time:G}: DoorWorker is doing additional work for closing the door", action.Time);
        break;
    }
  }
}
