namespace GarageEvents.Door;

using GarageEvents.Messages;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Class with only responsibility to handle door events
public class DoorHandler(ILogger<DoorHandler> logger, IRemote remote) : IDoorHandler
{
  public DoorHandler StartListen()
  {
    remote.GarageEvent += OnGarageEvent;
    return this;
  }

  public void StopListen() => remote.GarageEvent -= OnGarageEvent;

  private void OnGarageEvent(object sender, RemoteAction e)
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
