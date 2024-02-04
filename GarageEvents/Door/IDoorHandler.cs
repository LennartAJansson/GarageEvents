namespace GarageEvents.Door;

public interface IDoorHandler
{
  DoorHandler StartListen();
  void StopListen();
}
