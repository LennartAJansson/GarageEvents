namespace GarageEvents.Garage;
//Interface for interacting with the garage
public interface IGarageHandler
{
  public bool DoorIsOpen { get; set; }
  public bool LightsAreOn { get; set; }
  GarageHandler StartListen();
  void StopListen();
}