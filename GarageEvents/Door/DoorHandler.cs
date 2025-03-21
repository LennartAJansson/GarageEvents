﻿namespace GarageEvents.Door;

using GarageEvents.Base;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the door
public class DoorHandler(ILogger<DoorHandler> logger, IRemote remote)
  : Listener(remote), IDoorHandler
{
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
        _ = Task.Run(remote.DoorIsOpen);
        break;
      case RemoteActionType.CloseDoorCmd:
        logger.LogInformation("{time:G}: Door is closing", action.Time);
        //TODO: Implement the door closing
        _ = Task.Run(remote.DoorIsClosed);
        break;
    }
  }
}