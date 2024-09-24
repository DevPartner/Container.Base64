using ContainerBase64.Contracts.Services;
using ContainerBase64.Infrastructure.Services;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.IntegrationTests.Services;

[TestFixture]
public class EncodingTaskManagerTests
{
    private EncodingTaskManager _taskManager;

    [SetUp]
    public void Setup()
    {
        _taskManager = new EncodingTaskManager();
    }

    [Test]
    public async Task AddEncodingTask_TaskAddedSuccessfully()
    {
        string key = "TestKey";
        IEncodingClientProxy client = Mock.Of<IEncodingClientProxy>();
        Func<IEncodingClientProxy, Task> func = _ => Task.CompletedTask;

        await _taskManager.AddEncodingTask(key, client, func);

        CancellationToken? token = _taskManager.GetCancellationToken(key);

        token.Should().NotBeNull();
    }

    [Test]
    public async Task CancelEncodingTask_TaskExists_CancelsSuccessfully()
    {
        string key = "ExistingKey";
        IEncodingClientProxy client = Mock.Of<IEncodingClientProxy>();
        Func<IEncodingClientProxy, Task> func = _ => Task.CompletedTask;

        await _taskManager.AddEncodingTask(key, client, func);
        await _taskManager.CancelEncodingTask(key);

    }
}
