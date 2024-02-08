namespace GarageEvents.Nats.Remote;

using GarageEvents.Nats.Configuration;
using GarageEvents.Nats.Serializer;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

using NATS.Client.Core;

using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;

public class NatsRemote
  : IRemote, IDisposable
{
  private readonly ILogger<NatsRemote> logger;
  public bool IsConnected { get; private set; }

  private readonly NatsConnection? connection;
  private readonly NatsJSContext? jetStream;
  private readonly NatsServiceConfig config;
  private bool disposedValue;

  public event RemoteActionDelegate? RemoteEvent;

  public NatsRemote(ILogger<NatsRemote> logger, NatsServiceConfig config, ILoggerFactory loggerFactory)
  {
    this.logger = logger;
    NatsOpts opts = NatsOpts.Default with
    {
      Url = $"nats://{config.Host}:{config.Port}",
      LoggerFactory = loggerFactory
    };
    connection = new NatsConnection(opts);
    jetStream = new NatsJSContext(connection);
    connection.ConnectionOpened += Connection_ConnectionOpened;
    connection.ConnectionDisconnected += Connection_ConnectionDisconnected;
    this.config = config;
    _ = Task.Run(async () => await jetStream.CreateStreamAsync(new StreamConfig(name: config.Stream, subjects: config.Subjects)));
    _ = Task.Run(Listen);
  }

  private ValueTask Connection_ConnectionDisconnected(object? sender, NatsEventArgs args)
  {
    IsConnected = false;
    return new ValueTask();
  }

  private ValueTask Connection_ConnectionOpened(object? sender, NatsEventArgs args)
  {
    IsConnected = true;
    return new ValueTask();
  }

  public async Task Listen()
  {
    CancellationTokenSource cts = new();

    Console.CancelKeyPress += (_, e) =>
    {
      e.Cancel = true;
      cts.Cancel();
    };

    if (jetStream is not null)
    {
      INatsJSConsumer consumer = await jetStream.CreateOrUpdateConsumerAsync(config.Stream, new ConsumerConfig(config.Consumer));
      while (!cts.Token.IsCancellationRequested)
      {
        await foreach (NatsJSMsg<RemoteActionMessage> jsMsg in consumer!.ConsumeAsync<RemoteActionMessage>(serializer: new GarageActionSerializer(), cancellationToken: cts.Token))
        {
          if (jsMsg.Data is not null)
          {
            RemoteEvent?.Invoke(this, jsMsg.Data);
            await jsMsg.AckAsync();
          }
        }
      }
    }
  }
  public async Task OpenDoor()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.OpenDoorCmd);
    logger.LogInformation("{now:G}: Remote is signalling OpenDoor", action.Time);
    _ = await SendEvent(action);
  }

  public async Task CloseDoor()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.CloseDoorCmd);
    logger.LogInformation("{now:G}: Remote is signalling CloseDoor", action.Time);
    _ = await SendEvent(action);
  }

  public async Task LightsOn()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.LightsOnCmd);
    logger.LogInformation("{now:G}: Remote is signalling LightsOn", action.Time);
    _ = await SendEvent(action);
  }

  public async Task LightsOff()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.LightsOffCmd);
    logger.LogInformation("{now:G}: Remote is signalling LightsOff", action.Time);
    _ = await SendEvent(action);
  }

  public async Task Refresh()
  {
    RemoteActionMessage action = RemoteActionMessage.Create(RemoteActionType.RefreshCmd);
    logger.LogInformation("{now:G}: Remote is signalling GetStatus", action.Time);
    _ = await SendEvent(action);
  }

  private async Task<ulong> SendEvent(RemoteActionMessage action)
  {
    if (jetStream is null)
    {
      return 0;
    }

    PubAckResponse ack = await jetStream.PublishAsync<RemoteActionMessage>(subject: config.Subject, serializer: new GarageActionSerializer(), data: action);
    ack.EnsureSuccess();

    return ack.Seq;
  }

  protected virtual void Dispose(bool disposing)
  {
    if (!disposedValue)
    {
      if (disposing)
      {
        if (connection is not null)
        {
          connection.ConnectionOpened -= Connection_ConnectionOpened;
          connection.ConnectionDisconnected -= Connection_ConnectionDisconnected;
          _ = connection.DisposeAsync().AsTask().Wait(connection.Opts.CommandTimeout);
        }
      }
      disposedValue = true;
    }
  }

  public void Dispose()
  {
    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    Dispose(disposing: true);
    GC.SuppressFinalize(this);
  }

  public Task DoorIsOpen() => throw new NotImplementedException();
  public Task DoorIsClosed() => throw new NotImplementedException();
  public Task LightIsOn() => throw new NotImplementedException();
  public Task LightIsOff() => throw new NotImplementedException();
}
