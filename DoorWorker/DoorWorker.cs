namespace DoorWorker;

using GarageEvents.Door;
using GarageEvents.Remote;
using GarageEvents.Types;

public class DoorWorker(ILogger<DoorWorker> logger, IDoorHandler handler, IRemote remote)
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
      case RemoteActionType.OpenDoorCmd:
        logger.LogInformation("{time:G}: DoorWorker is doing work for opening the door", action.Time);
        //TODO: Implement the door opening
        remote.DoorIsOpen().RunSynchronously();
        break;
      case RemoteActionType.CloseDoorCmd:
        logger.LogInformation("{time:G}: DoorWorker is doing work for closing the door", action.Time);
        //TODO: Implement the door closing
        remote.DoorIsClosed().RunSynchronously();
        break;
    }
  }
}
