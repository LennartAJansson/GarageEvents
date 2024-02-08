namespace GarageEvents.State;
using GarageEvents.Types;

public interface ICurrentStateHandler
{
  bool DoorIsOpen { get; set; }
  bool LightIsOn { get; set; }

  ICurrentStateHandler StartListen(RemoteActionDelegate? callback);
  void StopListen();

}
