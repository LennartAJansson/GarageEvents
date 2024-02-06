namespace GarageEvents.Remote;

using GarageEvents.Messages;
using GarageEvents.Types;

//This is a default implementation of the IRemote interface
//It is using traditional delegates and events to signal the remote events
//Suitable for a single instance application
//with all components in the same process
public class DefaultRemote : IRemote
{
  public event RemoteActionDelegate? RemoteEvent;

  public Task OpenDoor()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    Console.WriteLine($"{now:G}: Remote is signalling OpenDoor");
    SendEvent(this, RemoteAction.Create(now, RemoteActionType.OpenDoor));
    return Task.CompletedTask;
  }

  public Task CloseDoor()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    Console.WriteLine($"{now:G}: Remote is signalling CloseDoor");
    SendEvent(this, RemoteAction.Create(now, RemoteActionType.CloseDoor));
    return Task.CompletedTask;
  }

  public Task LightsOn()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    Console.WriteLine($"{now:G}: Remote is signalling LightsOn");
    SendEvent(this, RemoteAction.Create(now, RemoteActionType.LightsOn));
    return Task.CompletedTask;
  }

  public Task LightsOff()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    Console.WriteLine($"{now:G}: Remote is signalling LightsOff");
    SendEvent(this, RemoteAction.Create(now, RemoteActionType.LightsOff));
    return Task.CompletedTask;
  }

  private void SendEvent(object sender, RemoteAction action)
    => RemoteEvent?.Invoke(sender, action);
}