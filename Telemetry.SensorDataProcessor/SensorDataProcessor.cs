namespace Telemetry.SensorDataProcessor
{
    public class SensorDataProcessor : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<SensorDataProcessor> _logger;
        private readonly IConfiguration configuration;

        public SensorDataProcessor(ILogger<SensorDataProcessor> logger, IConfiguration _configuration, IServiceProvider _serviceProvider)
        {
            _logger = logger;
            configuration = _configuration;
            serviceProvider = _serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                string poolFolder = configuration["PoolFolder"];
                if (string.IsNullOrEmpty(poolFolder))
                {
                    _logger.LogWarning("PoolFolder is not set.");
                    return;
                }
                Telemetry.Business.Pool.ProcessPool(poolFolder,serviceProvider);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
