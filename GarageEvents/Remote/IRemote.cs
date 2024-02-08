namespace GarageEvents.Remote;

using GarageEvents.Types;

public interface IRemote
{
  event RemoteActionDelegate? RemoteEvent;

  Task OpenDoor();

  Task CloseDoor();

  Task LightsOn();

  Task LightsOff();

  Task Refresh();
  Task DoorIsOpen();
  Task DoorIsClosed();
  Task LightIsOn();
  Task LightIsOff();
}
