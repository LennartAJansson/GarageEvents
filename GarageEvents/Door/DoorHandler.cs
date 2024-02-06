namespace GarageEvents.Door;

using GarageEvents.Messages;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the door
public class DoorHandler(ILogger<DoorHandler> logger, IRemote remote) : IDoorHandler
{
  private RemoteActionDelegate? callback;

  public DoorHandler StartListen(RemoteActionDelegate callback)
  {
    this.callback = callback;
    remote.RemoteEvent += OnRemoteEvent;
    remote.RemoteEvent += callback;
    return this;
  }

  public void StopListen()
  {
    remote.RemoteEvent -= callback;
    remote.RemoteEvent -= OnRemoteEvent;
  }

  private void OnRemoteEvent(object sender, RemoteAction e)
  {
    switch (e.RemoteActionType)
    {
      case RemoteActionType.OpenDoor:
        logger.LogInformation("{time:G}: DoorHandler is opening door", e.Time);
        break;
      case RemoteActionType.CloseDoor:
        logger.LogInformation("{time:G}: DoorHandler is closing door", e.Time);
        break;
    }
  }
}
