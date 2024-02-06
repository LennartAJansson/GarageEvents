namespace GarageWorker;

using GarageEvents.Garage;

public class Worker(ILogger<Worker> logger, IGarage handler) : BackgroundService
{
  public override Task StartAsync(CancellationToken cancellationToken) =>
    //handler.StartListen();
    base.StartAsync(cancellationToken);

  public override Task StopAsync(CancellationToken cancellationToken) =>
    //handler.StopListen();
    base.StopAsync(cancellationToken);

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
