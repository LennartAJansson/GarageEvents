namespace GarageEvents.Door;

using GarageEvents.Base;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the door
public class DoorHandler(ILogger<DoorHandler> logger, IRemote remote)
  : Listener(remote), IDoorHandler
{
  private readonly ILogger<DoorHandler> logger = logger;

  public IDoorHandler StartListen(RemoteActionDelegate? callback)
  {
    base.Start(callback);
    return this;
  }

  public override void OnRemoteEvent(object sender, RemoteActionMessage action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.OpenDoorCmd:
        logger.LogInformation("{time:G}: Door is opening", action.Time);
        //TODO: Implement the door opening
        remote.DoorIsOpen().RunSynchronously();
        break;
      case RemoteActionType.CloseDoorCmd:
        logger.LogInformation("{time:G}: Door is closing", action.Time);
        //TODO: Implement the door closing
        remote.DoorIsClosed().RunSynchronously();
        break;
    }
  }
}