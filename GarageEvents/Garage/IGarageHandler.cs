namespace GarageEvents.Garage;
using GarageEvents.Remote;

//Interface for interacting with the garage
public interface IGarageHandler
{
  public bool DoorIsOpen { get; set; }
  public bool LightsAreOn { get; set; }
  IGarageHandler StartListen(RemoteActionDelegate? callback);
  void StopListen();
}