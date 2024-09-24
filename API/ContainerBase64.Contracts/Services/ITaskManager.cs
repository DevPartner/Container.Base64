
namespace ContainerBase64.Contracts.Services;
public interface ITaskManager<T>
{
    Task AddEncodingTask(string key, T client, Func<T, Task> encodingTaskFunc);
    Task CancelEncodingTask(string key);
    CancellationToken? GetCancellationToken(string key);
}