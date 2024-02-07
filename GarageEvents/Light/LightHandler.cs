namespace GarageEvents.Light;

using GarageEvents.Base;
using GarageEvents.Messages;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the light
public class LightHandler(ILogger<LightHandler> logger, IRemote remote)
  : Listener(remote), ILightHandler
{
  private readonly ILogger<LightHandler> logger = logger;

  public LightHandler StartListen(RemoteActionDelegate? callback)
  {
    base.Start(callback);
    return this;
  }

  public override void OnRemoteEvent(object sender, RemoteAction action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.LightsOn:
        logger.LogInformation("{time:G}: Light is turning on", action.Time);
        break;
      case RemoteActionType.LightsOff:
        logger.LogInformation("{time:G}: Light is turning off", action.Time);
        break;
    }
  }

  ILightHandler ILightHandler.StartListen(RemoteActionDelegate callback) => throw new NotImplementedException();
}