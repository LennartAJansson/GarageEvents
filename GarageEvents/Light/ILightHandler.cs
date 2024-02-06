namespace GarageEvents.Light;

using GarageEvents.Remote;

public interface ILightHandler
{
  LightHandler StartListen(RemoteActionDelegate callback);
  void StopListen();
}
