using Skillx.Modules.Auth.Constants;
using Skillx.Modules.Auth.Core.Jwt;
using Skillx.Modules.Auth.Core.Options;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skillx.Modules.Auth.Jwt
{
    public class TokenGenerator : ITokenGenerator
    {
        public async Task<string> GenerateJwt(ClaimsIdentity identity, 
                                              IJwtFactory jwtFactory, 
                                              string userName, 
                                              JwtIssuerOptions jwtOptions, 
                                              JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == ConfigConstants.Jwt.Id).Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
