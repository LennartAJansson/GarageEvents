namespace GarageEvents.Base;
using GarageEvents.Messages;
using GarageEvents.Remote;

public abstract class Listener
  : IListener
{
  private readonly IRemote remote;
  private RemoteActionDelegate? callback = null;

  public Listener(IRemote remote) => this.remote = remote;

  public virtual object StartListen(RemoteActionDelegate? callback)
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

    return this;
  }

  public void StopListen()
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
}
