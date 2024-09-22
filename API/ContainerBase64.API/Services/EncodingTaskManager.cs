using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Base64ConverterSolution.Web.Services;

public class EncodingTaskManager : ITaskManager<ISingleClientProxy>
{
    private readonly ConcurrentDictionary<string, (Task EncodingTask, CancellationTokenSource CancellationTokenSource)> _encodingTasks = new();

    // Add a new encoding task
    public async Task AddEncodingTask(string key, ISingleClientProxy client, Func<ISingleClientProxy, Task> encodingTaskFunc)
    {
        var cancellationTokenSource = new CancellationTokenSource();

        var encodingTask = Task.Run(async () =>
        {
            await encodingTaskFunc(client);
        });

        _encodingTasks[key] = (encodingTask, cancellationTokenSource);

        await Task.CompletedTask; // Ensure method is async
    }

    // Cancel a specific encoding task
    public async Task CancelEncodingTask(string key)
    {
        if (_encodingTasks.TryGetValue(key, out var taskInfo))
        {
            taskInfo.CancellationTokenSource.Cancel();

            try
            {
                await taskInfo.EncodingTask;
            }
            catch (TaskCanceledException)
            {
                // Handle the case when the task was cancelled
            }
            finally
            {
                // Remove the task from the dictionary after cancellation
                _encodingTasks.TryRemove(key, out _);
            }
        }
    }

    public CancellationToken? GetCancellationToken(string key)
    {
        if (_encodingTasks.TryGetValue(key, out var taskInfo))
        {
            return taskInfo.CancellationTokenSource.Token;
        }
        return null;
    }
}
