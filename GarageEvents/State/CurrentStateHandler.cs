namespace GarageEvents.State;

using GarageEvents.Base;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

public class CurrentStateHandler(ILogger<CurrentStateHandler> logger, IRemote remote)
  : Listener(remote), ICurrentStateHandler
{
  public bool DoorIsOpen { get; set; }
  public bool LightIsOn { get; set; }

  public ICurrentStateHandler StartListen(RemoteActionDelegate? callback)
  {
    base.Start(callback);
    return this;
  }

  public override void OnRemoteEvent(object sender, RemoteActionMessage action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.DoorIsOpen:
        DoorIsOpen = true;
        remote.DoorIsOpen().RunSynchronously();
        logger.LogInformation("{time:G}: Door is open", action.Time);
        break;
      case RemoteActionType.DoorIsClosed:
        DoorIsOpen = false;
        remote.DoorIsClosed().RunSynchronously();
        logger.LogInformation("{time:G}: Door is closed", action.Time);
        break;
      case RemoteActionType.LightIsOn:
        LightIsOn = true;
        remote.LightIsOn().RunSynchronously();
        logger.LogInformation("{time:G}: Light is on", action.Time);
        break;
      case RemoteActionType.LightIsOff:
        LightIsOn = false;
        remote.LightIsOff().RunSynchronously();
        logger.LogInformation("{time:G}: Light is off", action.Time);
        break;
    }
  }
}
