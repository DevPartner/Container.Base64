using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Base64ConverterSolution.Web.Services;

public class EncodingService : IEncodingService
{
    private readonly ITaskManager<ISingleClientProxy> _taskManager;

    public EncodingService(ITaskManager<ISingleClientProxy> taskManager)
    {
        _taskManager = taskManager;
    }
    public async Task ProcessEncodingAsync(ISingleClientProxy client, string result, string key)
    {
        try
        {
            foreach (var ch in result)
            {
                var token = _taskManager.GetCancellationToken(key);
                if (token?.IsCancellationRequested ?? true)
                {
                    await client.SendAsync("ReceiveCancellationNotice");
                    break;
                }

                await client.SendAsync("ReceiveChar", ch);

                // Simulate processing delay
                await Task.Delay(new Random().Next(1000, 5000), token.Value);
            }
            await client.SendAsync("ReceiveSuccessNotice");
        }
        catch (TaskCanceledException)
        {
            await client.SendAsync("ReceiveCancellationNotice");
        }
    }
}
