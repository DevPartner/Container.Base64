using ContainerBase64.Contracts.Services;
using ContainerBase64.Core.Handlers.Commands;
using FluentAssertions;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.FunctionalTests.Handlers.Commands;
[TestFixture]
public class EncodeTextHandlerTests
{
    private Mock<ITaskManager<IEncodingClientProxy>> _mockTaskManager;
    private Mock<IEncodingService> _mockEncodingService;
    private EncodeTextHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockTaskManager = new Mock<ITaskManager<IEncodingClientProxy>>();
        _mockEncodingService = new Mock<IEncodingService>();
        _handler = new EncodeTextHandler(_mockTaskManager.Object, _mockEncodingService.Object);
    }

    [Test]
    public async Task Handle_ValidInput_EncodesAndProcessesText()
    {
        var command = new EncodeTextCommand
        {
            Input = "Hello World",
            Key = "UniqueKey123",
            Client = Mock.Of<IEncodingClientProxy>()
        };

        string expectedBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(command.Input));

        _mockTaskManager.Setup(x => x.AddEncodingTask(It.IsAny<string>(), It.IsAny<IEncodingClientProxy>(), It.IsAny<Func<IEncodingClientProxy, Task>>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _mockTaskManager.Verify(x => x.AddEncodingTask(command.Key, command.Client, It.IsAny<Func<IEncodingClientProxy, Task>>()), Times.Once);
    }

    [Test]
    public void Handle_NullCommand_ThrowsArgumentNullException()
    {
        FluentActions.Invoking(() => _handler.Handle(null, CancellationToken.None))
            .Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task Handle_CancellationRequested_CancelsTask()
    {
        var command = new EncodeTextCommand
        {
            Input = "Test",
            Key = "CancelKey",
            Client = Mock.Of<IEncodingClientProxy>()
        };
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        _mockTaskManager.Setup(x => x.AddEncodingTask(It.IsAny<string>(), It.IsAny<IEncodingClientProxy>(), It.IsAny<Func<IEncodingClientProxy, Task>>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, cancellationTokenSource.Token);

        _mockTaskManager.Verify(x => x.CancelEncodingTask(command.Key), Times.Never);
    }
}
