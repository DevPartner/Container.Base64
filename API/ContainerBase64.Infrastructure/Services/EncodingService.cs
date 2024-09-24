using ContainerBase64.Contracts.Services;

namespace ContainerBase64.Infrastructure.Services;

public class EncodingService : IEncodingService
{
    private readonly ITaskManager<IEncodingClientProxy> _taskManager;

    public EncodingService(ITaskManager<IEncodingClientProxy> taskManager)
    {
        _taskManager = taskManager;
    }
    public async Task ProcessEncodingAsync(IEncodingClientProxy client, string result, string key)
    {
        try
        {
            foreach (var ch in result)
            {
                var token = _taskManager.GetCancellationToken(key);
                if (token?.IsCancellationRequested ?? true)
                {
                    await client.SendAsync("ReceiveCancellationNotice", token);
                    break;
                }

                await client.SendAsync("ReceiveChar", ch, token ?? default);

                // Simulate processing delay
                await Task.Delay(new Random().Next(1000, 5000), token ?? default);
            }
            await client.SendAsync("ReceiveSuccessNotice");
        }
        catch (Exception)
        {
            await client.SendAsync("ReceiveCancellationNotice");
        }
    }
}
