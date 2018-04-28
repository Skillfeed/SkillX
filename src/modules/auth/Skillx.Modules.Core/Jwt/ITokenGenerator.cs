using Skillx.Modules.Auth.Core.Options;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skillx.Modules.Auth.Core.Jwt
{
    public interface ITokenGenerator
    {
        Task<string> GenerateJwt(ClaimsIdentity identity,
                                 IJwtFactory jwtFactory,
                                 string userName,
                                 JwtIssuerOptions jwtOptions,
                                 JsonSerializerSettings serializerSettings);
    }
}
