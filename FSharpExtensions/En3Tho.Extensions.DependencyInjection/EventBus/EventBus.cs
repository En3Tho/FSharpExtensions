using System.Threading.Tasks;

namespace En3Tho.Extensions.DependencyInjection;

public interface IEventBus
{
    ValueTask Publish<T>(T value);
}

public interface IEventBus<T>
{
    ValueTask Publish(T value);
}

public interface IEventConsumer<T>
{
    ValueTask Consume(T value);
}