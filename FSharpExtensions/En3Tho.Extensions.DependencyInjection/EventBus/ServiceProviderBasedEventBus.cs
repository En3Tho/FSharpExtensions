using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace En3Tho.Extensions.DependencyInjection;

public record ServiceProviderBasedEventBus(IServiceProvider ServiceProvider) : IEventBus
{
    public ValueTask Publish<T>(T value)
    {
        var bus = ServiceProvider.GetRequiredService<IEventBus<T>>();
        return bus.Publish(value);
    }
}

public record InMemoryEventBus<T>(ILogger<T> Logger, IEnumerable<IEventConsumer<T>> Consumers) : IEventBus<T>
{
    private readonly IEventConsumer<T>[] _consumers = Consumers.ToArray();
    private readonly Channel<T>[] _channels = Consumers.Select(_ => Channel.CreateUnbounded<T>(new() { SingleReader = true })).ToArray();

    private async Task StartReader(ChannelReader<T> reader, IEventConsumer<T> Consumer, CancellationToken token)
    {
        await foreach (var message in reader.ReadAllAsync(token))
        {
            var _ = Consumer.Consume(message);
        }
    }

    public void Start(CancellationToken token)
    {
        for (var i = 0; i < _channels.Length; i++)
        {
            var _ = StartReader(_channels[i].Reader, _consumers[i], token);
        }
    }

    public ValueTask Publish(T value)
    {
        foreach (var channel in _channels)
        {
            var _ = channel.Writer.WriteAsync(value);
        }

        return ValueTask.CompletedTask;
    }
}