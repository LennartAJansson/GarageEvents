namespace GarageEvents.Garage;

using GarageEvents.Messages;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

//Implementation for interacting with the garage
public class Garage : IGarage
{
  private readonly ILogger<Garage> logger;
  private readonly IRemote remote;
  //private readonly DoorHandler doorHandler;
  //private readonly LightHandler lightHandler;
  private bool disposedValue;

  public bool DoorIsOpen { get; set; } = false;
  public bool LightsAreOn { get; set; } = false;

  public Garage(ILogger<Garage> logger, IRemote remote)
  {
    this.logger = logger;
    this.remote = remote;
    this.remote.GarageEvent += OnGarageEvent;
  }

  private void OnGarageEvent(object sender, RemoteAction action)
  {
    switch (action.RemoteActionType)
    {
      case RemoteActionType.OpenDoor:
        logger.LogInformation("{time:G}: Door is open to the garage", action.Time);
        DoorIsOpen = true;
        break;
      case RemoteActionType.CloseDoor:
        logger.LogInformation("{time:G}: Door is closed to the garage", action.Time);
        DoorIsOpen = false;
        break;
      case RemoteActionType.LightsOn:
        logger.LogInformation("{time:G}: Light is on in the garage", action.Time);
        LightsAreOn = true;
        break;
      case RemoteActionType.LightsOff:
        logger.LogInformation("{time:G}: Light is off in the garage", action.Time);
        LightsAreOn = false;
        break;
    }
  }

  protected virtual void Dispose(bool disposing)
  {
    if (!disposedValue)
    {
      if (disposing)
      {
        //lightHandler.StopListen();
        //doorHandler.StopListen();
        remote.GarageEvent -= OnGarageEvent;
      }
      // TODO: free unmanaged resources (unmanaged objects) and override finalizer
      disposedValue = true;
    }
  }

  public void Dispose()
  {
    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    Dispose(disposing: true);
    GC.SuppressFinalize(this);
  }
}
