using ContainerBase64.Contracts.Services;
using ContainerBase64.Infrastructure.Services;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.IntegrationTests.Services;
[TestFixture]
public class EncodingServiceTests
{
    private Mock<ITaskManager<IEncodingClientProxy>> _mockTaskManager;
    private EncodingService _encodingService;
    private IEncodingClientProxy _client;
    private string _testKey = "EncodingKey";

    [SetUp]
    public void Setup()
    {
        _mockTaskManager = new Mock<ITaskManager<IEncodingClientProxy>>();
        _encodingService = new EncodingService(_mockTaskManager.Object);
        _client = Mock.Of<IEncodingClientProxy>();
    }

    [Test]
    public async Task ProcessEncodingAsync_NoCancellation_CompletesSuccessfully()
    {
        string result = "SGVsbG8gV29ybGQ="; // Base64 of "Hello World"
        _mockTaskManager.Setup(x => x.GetCancellationToken(It.IsAny<string>()))
            .Returns(() => new CancellationToken());

        await _encodingService.ProcessEncodingAsync(_client, result, _testKey);

        // Assume ReceiveChar and ReceiveSuccessNotice are methods we need to verify were called
        Mock.Get(_client).Verify(x => x.SendAsync("ReceiveChar", It.IsAny<char>(), default), Times.Exactly(result.Length));
        Mock.Get(_client).Verify(x => x.SendAsync("ReceiveSuccessNotice", default), Times.Once);
    }

    [Test]
    public async Task ProcessEncodingAsync_WithCancellation_CancelsMidway()
    {
        string result = "SGVsbG8gV29ybGQ=";
        var cancellationTokenSource = new CancellationTokenSource();

        _mockTaskManager.Setup(x => x.GetCancellationToken(It.IsAny<string>()))
            .Returns(() => cancellationTokenSource.Token);

        // Simulate cancellation midway
        var invocationCount = 0;
        Mock.Get(_client)
            .Setup(x => x.SendAsync("ReceiveChar", It.IsAny<char>(), It.IsAny<CancellationToken>()))
            .Callback(() => {
                invocationCount++;
                if (invocationCount == 6) cancellationTokenSource.Cancel(); // Cancel after a few sends
            })
            .Returns(Task.CompletedTask);

        await _encodingService.ProcessEncodingAsync(_client, result, _testKey);

        Mock.Get(_client).Verify(x => x.SendAsync("ReceiveCancellationNotice", It.IsAny<CancellationToken>()), Times.Once);
        Mock.Get(_client).Verify(x => x.SendAsync("ReceiveChar", It.IsAny<char>(), It.IsAny<CancellationToken>()), Times.AtMost(6));
    }

    [Test]
    public async Task ProcessEncodingAsync_ExceptionOccurs_HandlesGracefully()
    {
        string result = "SGVsbG8=";
        _mockTaskManager.Setup(x => x.GetCancellationToken(It.IsAny<string>()))
            .Returns(() => new CancellationToken());

        Mock.Get(_client)
            .Setup(x => x.SendAsync("ReceiveChar", It.IsAny<char>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Simulated Exception"));

        await _encodingService.ProcessEncodingAsync(_client, result, _testKey);

        Mock.Get(_client).Verify(x => x.SendAsync("ReceiveCancellationNotice", It.IsAny<CancellationToken>()), Times.Once);
    }
}