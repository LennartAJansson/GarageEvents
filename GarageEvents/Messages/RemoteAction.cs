namespace GarageEvents.Messages;

using System.Text.Json;

using GarageEvents.Types;

//Message for the garage event
public record RemoteAction(DateTimeOffset Time, RemoteActionType RemoteActionType)
{
  public static RemoteAction Create(DateTimeOffset time, RemoteActionType actionType)
    => new(time, actionType);
}

public static class RemoteActionExtensions
{
  public static string ToJson(this RemoteAction action)
  {
    string response = JsonSerializer.Serialize(action, options: JsonSerializerOptions.Default);
    return response;
  }

  public static RemoteAction? ToRemoteAction(this string json) => JsonSerializer.Deserialize<RemoteAction>(json);
}