using Skillx.Gateways.WebAPI.Models;
using System.Threading.Tasks;

namespace Skillx.Gateways.WebAPI.Services.Abstraction.Common
{
    public interface IApplicationHttpClient
    {
        Task<DefaultResponse> PostAsync(string url, object payload);
    }
}
