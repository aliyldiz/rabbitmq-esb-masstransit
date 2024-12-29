using MassTransit;
using RabbitMQ.ESB.MassTransit.WorkerService.Publisher.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("your-rabbitmq-connection-string");
            });
        });
        services.AddHostedService<PublishMessageService>(provider =>
        {
            using IServiceScope scope = provider.CreateScope();
            IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
            return new(publishEndpoint);
        });
    })
    .Build();

await host.RunAsync();