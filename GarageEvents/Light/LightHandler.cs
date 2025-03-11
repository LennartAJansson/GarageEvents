namespace GarageEvents.Light;

using GarageEvents.Base;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the light
public class LightHandler(ILogger<LightHandler> logger, IRemote remote)
  : Listener(remote), ILightHandler
{
  public ILightHandler StartListen(RemoteActionDelegate? callback)
  {
    base.Start(callback);

    return this;
  }

  public override void OnRemoteEvent(object sender, RemoteActionMessage action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.LightsOnCmd:
        logger.LogInformation("{time:G}: Light is turning on", action.Time);
        //TODO: Implement the light turning on
        _ = Task.Run(remote.LightIsOn);
        break;
      case RemoteActionType.LightsOffCmd:
        logger.LogInformation("{time:G}: Light is turning off", action.Time);
        //TODO: Implement the light turning off
        _ = Task.Run(remote.LightIsOff);
        break;
    }
  }
}