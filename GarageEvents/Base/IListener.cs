namespace GarageEvents.Base;

using GarageEvents.Remote;

public interface IListener
{
  object StartListen(RemoteActionDelegate callback);
  void StopListen();
}