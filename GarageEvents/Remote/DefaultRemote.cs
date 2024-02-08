namespace GarageEvents.Remote;

using GarageEvents.State;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//This is a default implementation of the IRemote interface
//It is using traditional delegates and events to signal the remote events
//Suitable for a single instance application
//with all components in the same process
public class DefaultRemote(ILogger<DefaultRemote> logger, CurrentStateHandler state)
  : IRemote
{
  public event RemoteActionDelegate? RemoteEvent;

  public Task OpenDoor()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.OpenDoorCmd);
    logger.LogInformation("{time:G}: Remote is signalling OpenDoor", action.Time);
    SendEvent(this, action);
    return Task.CompletedTask;
  }

  public Task CloseDoor()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.CloseDoorCmd);
    logger.LogInformation("{time:G}: Remote is signalling CloseDoor", action.Time);
    SendEvent(this, action);
    return Task.CompletedTask;
  }

  public Task LightsOn()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.LightsOnCmd);
    logger.LogInformation("{time:G}: Remote is signalling LightsOn", action.Time);
    SendEvent(this, action);
    return Task.CompletedTask;
  }

  public Task LightsOff()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.LightsOffCmd);
    logger.LogInformation("{time:G}: Remote is signalling LightsOff", action.Time);
    SendEvent(this, action);
    return Task.CompletedTask;
  }

  public Task Refresh()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.RefreshCmd);
    logger.LogInformation("{time:G}: Remote is signalling GetStatus", action.Time);
    SendEvent(this, action);
    return Task.CompletedTask;
  }

  private void SendEvent(object sender, RemoteActionMessage action)
    => RemoteEvent?.Invoke(sender, action);
}