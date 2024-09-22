using System;
using System.Threading;
using System.Threading.Tasks;

namespace Base64ConverterSolution.Web.Services;
public interface ITaskManager<TClient>
{
    Task AddEncodingTask(string key, TClient client, Func<TClient, Task> encodingTask);
    Task CancelEncodingTask(string key);
    CancellationToken? GetCancellationToken(string key);
}
