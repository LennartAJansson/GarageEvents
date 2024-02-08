namespace GarageEvents.Types;

using System.Text.Json;

public static class RemoteActionExtensions
{
  public static string ToJson(this RemoteActionMessage action)
  {
    string response = JsonSerializer.Serialize(action, options: JsonSerializerOptions.Default);
    return response;
  }

  public static RemoteActionMessage? ToRemoteAction(this string json) => JsonSerializer.Deserialize<RemoteActionMessage>(json);
}