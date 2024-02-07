namespace GarageEvents.Light;
using GarageEvents.Remote;

public interface ILightHandler
{
  ILightHandler StartListen(RemoteActionDelegate callback);
  void StopListen();
}
