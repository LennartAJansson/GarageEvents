namespace GarageEvents.Garage;

using GarageEvents.Messages;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the garage
public class GarageHandler : IGarageHandler
{
  private readonly ILogger<GarageHandler> logger;
  private readonly IRemote remote;
  private RemoteActionDelegate? callback = null;

  public bool DoorIsOpen { get; set; } = false;
  public bool LightsAreOn { get; set; } = false;

  public GarageHandler(ILogger<GarageHandler> logger, IRemote remote)
  {
    this.logger = logger;
    this.remote = remote;
  }

  public GarageHandler StartListen(RemoteActionDelegate callback)
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

  private void OnRemoteEvent(object sender, RemoteAction action)
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
