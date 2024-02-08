namespace GarageEvents.Light;
using GarageEvents.Types;

public interface ILightHandler
{
  ILightHandler StartListen(RemoteActionDelegate? callback);
  void StopListen();
}
