namespace GarageEvents.Light;

using GarageEvents.Base;
using GarageEvents.Messages;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the light
public class LightHandler : Listener, ILightHandler
{
  private readonly ILogger<LightHandler> logger;
  private readonly IRemote remote;
  private readonly RemoteActionDelegate? callback;

  public LightHandler(ILogger<LightHandler> logger, IRemote remote)
    : base(remote)
  {
    this.logger = logger;
    this.remote = remote;
  }

  public override LightHandler StartListen(RemoteActionDelegate? callback)
  {
    _ = base.StartListen(callback);
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
}