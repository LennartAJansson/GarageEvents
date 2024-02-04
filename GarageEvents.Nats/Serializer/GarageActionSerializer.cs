namespace GarageEvents.Nats.Serializer;

using System.Buffers;
using System.Linq;
using System.Text.Json;

using GarageEvents.Messages;

using NATS.Client.Core;

internal class GarageActionSerializer : INatsSerializer<RemoteAction>
{
  public RemoteAction? Deserialize(in ReadOnlySequence<byte> buffer)
  {
    byte[] buf = buffer.ToArray();
    RemoteAction? action = JsonSerializer.Deserialize<RemoteAction>(buf);
    return action;
  }

  public void Serialize(IBufferWriter<byte> bufferWriter, RemoteAction value)
  {
    byte[] buf = JsonSerializer.SerializeToUtf8Bytes(value);
    bufferWriter.Write(buf);
  }
}
