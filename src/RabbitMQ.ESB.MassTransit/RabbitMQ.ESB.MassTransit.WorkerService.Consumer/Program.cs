using MassTransit;
using RabbitMQ.ESB.MassTransit.WorkerService.Consumer.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ExampleMessageConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("your-rabbitmq-connection-string");
                cfg.ReceiveEndpoint("example-queue", e =>
                {
                    e.ConfigureConsumer<ExampleMessageConsumer>(context);
                });
            });
        });
    })
    .Build();
    
await host.RunAsync();