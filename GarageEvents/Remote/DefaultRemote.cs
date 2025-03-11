namespace GarageEvents.Remote;

using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//This is a default implementation of the IRemote interface
//It is using traditional delegates and events to signal the remote events
//Suitable for a single instance application
//with all components in the same process
public class DefaultRemote(ILogger<DefaultRemote> logger)
  : IRemote
{
  public event RemoteActionDelegate? RemoteEvent;

  public Task OpenDoor()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.OpenDoorCmd);
    logger.LogInformation("{time:G}: Remote is signalling command OpenDoor", action.Time);
    SendEvent(this, action);

    return Task.CompletedTask;
  }

  public Task CloseDoor()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.CloseDoorCmd);
    logger.LogInformation("{time:G}: Remote is signalling command CloseDoor", action.Time);
    SendEvent(this, action);
    
    return Task.CompletedTask;
  }

  public Task LightsOn()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.LightsOnCmd);
    logger.LogInformation("{time:G}: Remote is signalling command LightsOn", action.Time);
    SendEvent(this, action);

    return Task.CompletedTask;
  }

  public Task LightsOff()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.LightsOffCmd);
    logger.LogInformation("{time:G}: Remote is signalling command LightsOff", action.Time);
    SendEvent(this, action);

    return Task.CompletedTask;
  }

  public Task Refresh()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.RefreshCmd);
    logger.LogInformation("{time:G}: Remote is signalling command Refresh", action.Time);
    SendEvent(this, action);

    return Task.CompletedTask;
  }
  public Task DoorIsOpen()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.DoorIsOpen);
    logger.LogInformation("{time:G}: Remote is signalling event DoorIsOpen", action.Time);
    SendEvent(this, action);

    return Task.CompletedTask;
  }

  public Task DoorIsClosed()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.DoorIsClosed);
    logger.LogInformation("{time:G}: Remote is signalling event DoorIsClosed", action.Time);
    SendEvent(this, action);

    return Task.CompletedTask;
  }

  public Task LightIsOn()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.LightIsOn);
    logger.LogInformation("{time:G}: Remote is signalling event LightIsOn", action.Time);
    SendEvent(this, action);
    
    return Task.CompletedTask;
  }

  public Task LightIsOff()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.LightIsOff);
    logger.LogInformation("{time:G}: Remote is signalling event LightIsOff", action.Time);
    SendEvent(this, action);

    return Task.CompletedTask;
  }

  private void SendEvent(object sender, RemoteActionMessage action)
    => RemoteEvent?.Invoke(sender, action);
}