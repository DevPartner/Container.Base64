using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ContainerBase64.Contracts.Services;
using ContainerBase64.API.Services;
using ContainerBase64.Core.Handlers.Commands;
using MediatR;

namespace ContainerBase64.API.SignalRHub;

public class EncodingHub : Hub
{
    private readonly ITaskManager<IEncodingClientProxy> _taskManager;
    private readonly IMediator _mediator;

    public EncodingHub(IMediator mediator, ITaskManager<IEncodingClientProxy> taskManager)
    {
        _mediator = mediator;
        _taskManager = taskManager;
    }

    public async Task EncodeText(string input, string operationId)
    {
        var clientProxy = new SignalRClientProxy(Clients.Caller);
        var key = Context.ConnectionId + operationId;
        var command = new EncodeTextCommand
        {
            Input = input,
            Key = key,
            Client = clientProxy 
        };
        await _mediator.Send(command);
    }

    public async Task CancelEncoding(string operationId)
    {
        var key = Context.ConnectionId + operationId;
        await _taskManager.CancelEncodingTask(key);
    }
}
