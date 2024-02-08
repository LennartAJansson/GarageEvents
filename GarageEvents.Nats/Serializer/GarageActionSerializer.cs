namespace GarageEvents.Nats.Serializer;

using System.Buffers;
using System.Linq;
using System.Text.Json;

using GarageEvents.Types;

using NATS.Client.Core;

internal class GarageActionSerializer : INatsSerializer<RemoteActionMessage>
{
  public RemoteActionMessage? Deserialize(in ReadOnlySequence<byte> buffer)
  {
    byte[] buf = buffer.ToArray();
    RemoteActionMessage? action = JsonSerializer.Deserialize<RemoteActionMessage>(buf);
    return action;
  }

  public void Serialize(IBufferWriter<byte> bufferWriter, RemoteActionMessage value)
  {
    byte[] buf = JsonSerializer.SerializeToUtf8Bytes(value);
    bufferWriter.Write(buf);
  }
}
