using Base64ConverterSolution.Web.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Base64ConverterSolution.Web.SignalRHub;

public class EncodingHub : Hub
{
    private readonly ITaskManager<ISingleClientProxy> _taskManager;
    private readonly IEncodingService _encodingService;

    public EncodingHub(ITaskManager<ISingleClientProxy> taskManager, IEncodingService encodingService)
    {
        _taskManager = taskManager;
        _encodingService = encodingService;
    }

    public async Task EncodeText(string input, string operationId)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var key = Context.ConnectionId + operationId;

        var base64Encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(input));

        await _taskManager.AddEncodingTask(key, Clients.Caller, async client =>
        {
            //await LongRunningTask(client, result, key);
            await _encodingService.ProcessEncodingAsync(client, base64Encoded, key);
        });
    }

    public async Task CancelEncoding(string operationId)
    {
        var key = Context.ConnectionId + operationId;
        await _taskManager.CancelEncodingTask(key);
    }
}
