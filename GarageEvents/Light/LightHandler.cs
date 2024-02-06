namespace GarageEvents.Light;

using GarageEvents.Messages;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the light
public class LightHandler(ILogger<LightHandler> logger, IRemote remote) : ILightHandler
{
  private RemoteActionDelegate? callback;

  public LightHandler StartListen(RemoteActionDelegate callback)
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
      case RemoteActionType.LightsOn:
        logger.LogInformation("{time:G}: LightHandler is turning on lights", action.Time);
        break;
      case RemoteActionType.LightsOff:
        logger.LogInformation("{time:G}: LightHandler is turning off lights", action.Time);
        break;
    }
  }
}