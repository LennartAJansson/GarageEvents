namespace GarageEvents.Light;

public interface ILightHandler
{
  LightHandler StartListen();
  void StopListen();
}
