namespace GarageEvents.Base;
using GarageEvents.Remote;
using GarageEvents.Types;

public abstract class Listener(IRemote remote)
{
  protected readonly IRemote remote = remote;
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

  public void StopListen() => Stop();
}
