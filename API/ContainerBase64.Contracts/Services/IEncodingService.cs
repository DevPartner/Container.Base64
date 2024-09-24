
namespace ContainerBase64.Contracts.Services;

public interface IEncodingService
{
    Task ProcessEncodingAsync(IEncodingClientProxy client, string result, string key);
}