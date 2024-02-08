namespace GarageEvents.Types;

//Message for the remote events
public record RemoteActionMessage(Guid Id, DateTimeOffset Time, RemoteActionType RemoteActionType)
{
  public static RemoteActionMessage Create(RemoteActionType actionType)
    => new(Guid.NewGuid(), DateTimeOffset.UtcNow, actionType);
}
