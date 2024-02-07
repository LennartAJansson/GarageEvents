namespace GarageEvents.Door;

using GarageEvents.Remote;

public interface IDoorHandler
{
  IDoorHandler StartListen(RemoteActionDelegate? callback);
  void StopListen();
}
