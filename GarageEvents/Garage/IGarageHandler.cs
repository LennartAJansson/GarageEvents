namespace GarageEvents.Garage;

using GarageEvents.Base;

//Interface for interacting with the garage
public interface IGarageHandler : IListener
{
  public bool DoorIsOpen { get; set; }
  public bool LightsAreOn { get; set; }
}