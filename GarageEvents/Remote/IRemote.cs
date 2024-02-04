namespace GarageEvents.Remote;

using GarageEvents.Messages;

//Delegate for the garage event
public delegate void RemoteActionDelegate(object sender, RemoteAction action);

public interface IRemote
{
  event RemoteActionDelegate? GarageEvent;

  Task OpenDoor();

  Task CloseDoor();

  Task LightsOn();

  Task LightsOff();
}
