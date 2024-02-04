namespace GarageEvents.Nats.Remote;

using GarageEvents.Messages;
using GarageEvents.Nats.Configuration;
using GarageEvents.Nats.Serializer;
using GarageEvents.Remote;
using GarageEvents.Types;

using Microsoft.Extensions.Logging;

using NATS.Client.Core;

using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;

public class NatsRemote : IRemote, IDisposable
{
  private readonly ILogger<NatsRemote> logger;
  public bool IsConnected { get; private set; }

  private readonly NatsConnection? connection;
  private readonly NatsJSContext? jetStream;
  private readonly NatsServiceConfig config;
  private bool disposedValue;

  public event RemoteActionDelegate? GarageEvent;

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
        await foreach (NatsJSMsg<RemoteAction> jsMsg in consumer!.ConsumeAsync<RemoteAction>(serializer: new GarageActionSerializer(), cancellationToken: cts.Token))
        {
          if (jsMsg.Data is not null)
          {
            GarageEvent?.Invoke(this, jsMsg.Data);
            await jsMsg.AckAsync();
          }
        }
      }
    }
  }
  public async Task OpenDoor()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    logger.LogInformation("{now:G}: Remote is signalling OpenDoor", now);
    _ = await SendEvent(RemoteAction.Create(now, RemoteActionType.OpenDoor));
  }

  public async Task CloseDoor()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    logger.LogInformation("{now:G}: Remote is signalling CloseDoor", now);
    _ = await SendEvent(RemoteAction.Create(now, RemoteActionType.CloseDoor));
  }

  public async Task LightsOn()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    logger.LogInformation("{now:G}: Remote is signalling LightsOn", now);
    _ = await SendEvent(RemoteAction.Create(now, RemoteActionType.LightsOn));
  }

  public async Task LightsOff()
  {
    DateTimeOffset now = DateTimeOffset.Now;
    logger.LogInformation("{now:G}: Remote is signalling LightsOff", now);
    _ = await SendEvent(RemoteAction.Create(now, RemoteActionType.LightsOff));
  }

  private async Task<ulong> SendEvent(RemoteAction action)
  {
    if (jetStream is null)
    {
      return 0;
    }

    PubAckResponse ack = await jetStream.PublishAsync<RemoteAction>(subject: config.Subject, serializer: new GarageActionSerializer(), data: action);
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

      // TODO: free unmanaged resources (unmanaged objects) and override finalizer
      // TODO: set large fields to null
      disposedValue = true;
    }
  }

  // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
  // ~NatsRemote()
  // {
  //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
  //     Dispose(disposing: false);
  // }

  public void Dispose()
  {
    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    Dispose(disposing: true);
    GC.SuppressFinalize(this);
  }
}
