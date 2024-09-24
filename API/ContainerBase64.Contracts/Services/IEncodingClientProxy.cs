namespace ContainerBase64.Contracts.Services;
public interface IEncodingClientProxy
{
    Task SendAsync(string methodName, object arg, CancellationToken cancellationToken = default);
    Task SendAsync(string methodName, CancellationToken cancellationToken = default);
}
