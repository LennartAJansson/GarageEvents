namespace GarageEvents.Remote;

using GarageEvents.Messages;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//This is a default implementation of the IRemote interface
//It is using traditional delegates and events to signal the remote events
//Suitable for a single instance application
//with all components in the same process
public class DefaultRemote(ILogger<DefaultRemote> logger) : IRemote
{
  public event RemoteActionDelegate? RemoteEvent;

  public Task OpenDoor()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    logger.LogInformation("{time:G}: Remote is signalling OpenDoor", now);
    SendEvent(this, RemoteAction.Create(now, RemoteActionType.OpenDoor));
    return Task.CompletedTask;
  }

  public Task CloseDoor()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    logger.LogInformation("{time:G}: Remote is signalling CloseDoor", now);
    SendEvent(this, RemoteAction.Create(now, RemoteActionType.CloseDoor));
    return Task.CompletedTask;
  }

  public Task LightsOn()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    logger.LogInformation("{time:G}: Remote is signalling LightsOn", now);
    SendEvent(this, RemoteAction.Create(now, RemoteActionType.LightsOn));
    return Task.CompletedTask;
  }

  public Task LightsOff()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    logger.LogInformation("{time:G}: Remote is signalling LightsOff", now);
    SendEvent(this, RemoteAction.Create(now, RemoteActionType.LightsOff));
    return Task.CompletedTask;
  }

  private void SendEvent(object sender, RemoteAction action)
    => RemoteEvent?.Invoke(sender, action);
}