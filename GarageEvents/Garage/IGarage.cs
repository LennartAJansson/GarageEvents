namespace GarageEvents.Garage;

//Interface for interacting with the garage
public interface IGarage : IDisposable
{
  public bool DoorIsOpen { get; set; }
  public bool LightsAreOn { get; set; }
}