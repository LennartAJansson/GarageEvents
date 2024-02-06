namespace DoorWorker;

using GarageEvents.Door;

public class Worker(ILogger<Worker> logger, IDoorHandler handler) : BackgroundService
{
  public override Task StartAsync(CancellationToken cancellationToken)
  {
    _ = handler.StartListen();
    return base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    handler.StopListen();
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      if (logger.IsEnabled(LogLevel.Information))
      {
        logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
      }
      await Task.Delay(1000, stoppingToken);
    }
  }
}
