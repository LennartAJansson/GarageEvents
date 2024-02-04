namespace GarageEvents.Light;

using GarageEvents.Messages;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Class with only responsibility to handle light events
public class LightHandler(ILogger<LightHandler> logger, IRemote remote) : ILightHandler
{
  public LightHandler StartListen()
  {
    remote.GarageEvent += OnGarageEvent;
    return this;
  }

  public void StopListen() => remote.GarageEvent -= OnGarageEvent;

  private void OnGarageEvent(object sender, RemoteAction action)
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