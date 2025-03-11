namespace GarageEvents.Base;

using GarageEvents.Remote;
using GarageEvents.Types;

public abstract class Listener(IRemote remote)
{
  private RemoteActionDelegate? callback = null;

  protected void Start(RemoteActionDelegate? callback)
  {
    if (callback is null)
    {
      remote.RemoteEvent += OnRemoteEvent;
    }
    else
    {
      this.callback = callback;
      remote.RemoteEvent += callback;
    }
  }

  public void StopListen() => Stop();
  
  protected void Stop()
  {
    if (callback is not null)
    {
      remote.RemoteEvent -= callback;
    }
    else
    {
      remote.RemoteEvent -= OnRemoteEvent;
    }
  }

  public abstract void OnRemoteEvent(object sender, RemoteActionMessage action);
}
