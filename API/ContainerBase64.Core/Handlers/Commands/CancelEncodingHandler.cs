using ContainerBase64.Contracts.Services;
using MediatR;

namespace ContainerBase64.Core.Handlers.Commands;

public class CancelEncodingCommand : IRequest
{
    public required string Key { get; set; }
}
public class CancelEncodingCommandHandler : IRequestHandler<CancelEncodingCommand>
{
    private readonly ITaskManager<IEncodingClientProxy> _taskManager;

    public CancelEncodingCommandHandler(ITaskManager<IEncodingClientProxy> taskManager)
    {
        _taskManager = taskManager;
    }

    public async Task Handle(CancelEncodingCommand request, CancellationToken cancellationToken)
    {
        await _taskManager.CancelEncodingTask(request.Key);
    }
}
