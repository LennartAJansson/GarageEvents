namespace GarageEvents.Base;
using GarageEvents.Messages;
using GarageEvents.Remote;

public abstract class Listener(IRemote remote)
{
  private readonly IRemote remote = remote;
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

  public abstract void OnRemoteEvent(object sender, RemoteAction action);

  public void StopListen() => Stop();
}
