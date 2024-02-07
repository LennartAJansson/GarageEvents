﻿namespace GarageEvents.Garage;

using GarageEvents.Base;
using GarageEvents.Messages;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the garage
public class GarageHandler(ILogger<GarageHandler> logger, IRemote remote)
  : Listener(remote), IGarageHandler
{
  private readonly ILogger<GarageHandler> logger = logger;

  public bool DoorIsOpen { get; set; } = false;
  public bool LightsAreOn { get; set; } = false;

  public IGarageHandler StartListen(RemoteActionDelegate? callback)
  {
    base.Start(callback);
    return this;
  }

  public override void OnRemoteEvent(object sender, RemoteAction action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.OpenDoor:
        logger.LogInformation("{time:G}: Door is open to the garage", action.Time);
        DoorIsOpen = true;
        break;
      case RemoteActionType.CloseDoor:
        logger.LogInformation("{time:G}: Door is closed to the garage", action.Time);
        DoorIsOpen = false;
        break;
      case RemoteActionType.LightsOn:
        logger.LogInformation("{time:G}: Light is on in the garage", action.Time);
        LightsAreOn = true;
        break;
      case RemoteActionType.LightsOff:
        logger.LogInformation("{time:G}: Light is off in the garage", action.Time);
        LightsAreOn = false;
        break;
    }
  }
}
