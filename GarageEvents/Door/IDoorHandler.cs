namespace GarageEvents.Door;

using GarageEvents.Types;

public interface IDoorHandler
{
  IDoorHandler StartListen(RemoteActionDelegate? callback);
  void StopListen();
}
