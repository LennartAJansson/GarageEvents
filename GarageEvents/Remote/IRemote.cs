namespace GarageEvents.Remote;

using GarageEvents.Types;

public interface IRemote
{
  //Event for the garage
  event RemoteActionDelegate? RemoteEvent;

  //Commands from the remote
  Task OpenDoor();
  Task CloseDoor();
  Task LightsOn();
  Task LightsOff();
  Task Refresh();

  //Events from remote
  Task DoorIsOpen();
  Task DoorIsClosed();
  Task LightIsOn();
  Task LightIsOff();
}
