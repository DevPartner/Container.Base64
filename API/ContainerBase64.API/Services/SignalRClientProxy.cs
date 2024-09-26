using ContainerBase64.Contracts.Services;
using Microsoft.AspNetCore.SignalR;

namespace ContainerBase64.API.Services;
public class SignalRClientProxy : IEncodingClientProxy
{
    private readonly ISingleClientProxy _client;

    public SignalRClientProxy(ISingleClientProxy client)
    {
        _client = client;
    }

    public Task SendAsync(string methodName, object arg, CancellationToken cancellationToken = default)
    {
        return _client.SendAsync(methodName, arg, cancellationToken);
    }

    public Task SendAsync(string methodName, CancellationToken cancellationToken = default)
    {
        return _client.SendAsync(methodName, cancellationToken);
    }
}