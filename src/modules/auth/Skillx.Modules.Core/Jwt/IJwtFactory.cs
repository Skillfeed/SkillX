using System.Security.Claims;
using System.Threading.Tasks;

namespace Skillx.Modules.Auth.Core.Jwt
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);

        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
