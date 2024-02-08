namespace GarageEvents.Light;

using GarageEvents.Base;
using GarageEvents.Remote;
using GarageEvents.State;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the light
public class LightHandler(ILogger<LightHandler> logger, IRemote remote, CurrentStateHandler state)
  : Listener(remote), ILightHandler
{
  private readonly ILogger<LightHandler> logger = logger;

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
        remote.LightIsOn().RunSynchronously();
        break;
      case RemoteActionType.LightsOffCmd:
        logger.LogInformation("{time:G}: Light is turning off", action.Time);
        //TODO: Implement the light turning off
        remote.LightIsOff().RunSynchronously();
        break;
    }
  }
}