namespace GarageEvents.Door;

using GarageEvents.Base;
using GarageEvents.Messages;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the door
public class DoorHandler(ILogger<DoorHandler> logger, IRemote remote)
  : Listener(remote), IDoorHandler
{
  private readonly ILogger<DoorHandler> logger = logger;

  public DoorHandler StartListen(RemoteActionDelegate? callback)
  {
    base.Start(callback);
    return this;
  }

  public override void OnRemoteEvent(object sender, RemoteAction action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.OpenDoor:
        logger.LogInformation("{time:G}: Door is opening", action.Time);
        break;
      case RemoteActionType.CloseDoor:
        logger.LogInformation("{time:G}: Door is closing", action.Time);
        break;
    }
  }

  IDoorHandler IDoorHandler.StartListen(RemoteActionDelegate callback) => throw new NotImplementedException();
}
