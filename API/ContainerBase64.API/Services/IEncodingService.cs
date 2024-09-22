using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Base64ConverterSolution.Web.Services;

public interface IEncodingService
{
    Task ProcessEncodingAsync(ISingleClientProxy client, string result, string key);
}
