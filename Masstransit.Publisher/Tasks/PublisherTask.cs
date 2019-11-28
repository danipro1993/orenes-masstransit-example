using Masstransit.Publisher.Messages;
using MassTransit;
using Messages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Masstransit.Publisher.Tasks
{
    public class PublisherTask : BackgroundService
    {
        private readonly ILogger<PublisherTask> _logger;
        private readonly IBus _bus;

        public PublisherTask(ILogger<PublisherTask> logger, IBus bus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"PublisherTask is starting.");

            stoppingToken.Register(() =>
                    _logger.LogDebug($" PublisherTask background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"PublisherTask task doing background work.");

                // This eShopOnContainers method is quering a database table 
                // and publishing events into the Event Bus (RabbitMS / ServiceBus)
                await _bus.Publish(new MessagePublisher($"Hola rabbitmq {new Random().Next(0, 1000)}!!!"));

                await Task.Delay(5000, stoppingToken);
            }

            _logger.LogDebug($"PublisherTask background task is stopping.");
        }
    }
}
