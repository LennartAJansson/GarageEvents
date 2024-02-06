namespace GarageEvents.Door;

using GarageEvents.Remote;

public interface IDoorHandler
{
  DoorHandler StartListen(RemoteActionDelegate callback);
  void StopListen();
}
