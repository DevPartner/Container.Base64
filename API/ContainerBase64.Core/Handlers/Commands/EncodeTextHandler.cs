using ContainerBase64.Contracts.Services;
using MediatR;
using System.Text;

namespace ContainerBase64.Core.Handlers.Commands
{
    public class EncodeTextCommand : IRequest
    {
        public required string Input { get; set; }
        public required string Key { get; set; }
        public required IEncodingClientProxy Client { get; set; }
    }

    public class EncodeTextHandler : IRequestHandler<EncodeTextCommand>
    {
        private readonly ITaskManager<IEncodingClientProxy> _taskManager;
        private readonly IEncodingService _encodingService;

        public EncodeTextHandler(ITaskManager<IEncodingClientProxy> taskManager, IEncodingService encodingService)
        {
            _taskManager = taskManager;
            _encodingService = encodingService;
        }

        public async Task Handle(EncodeTextCommand request, CancellationToken cancellationToken)
        {
            var base64Encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Input));

            await _taskManager.AddEncodingTask(request.Key, request.Client, async client =>
            {
                await _encodingService.ProcessEncodingAsync(client, base64Encoded, request.Key);
            });
        }
    }
}
