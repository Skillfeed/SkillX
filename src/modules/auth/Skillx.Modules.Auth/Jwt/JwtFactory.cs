using Microsoft.Extensions.Options;
using Skillx.Modules.Auth.Constants;
using Skillx.Modules.Auth.Core.Jwt;
using Skillx.Modules.Auth.Core.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Skillx.Modules.Auth.Jwt
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(ConfigConstants.Jwt.Rol),
                 identity.FindFirst(ConfigConstants.Jwt.Id)
            };

            var jwt = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                notBefore: jwtOptions.NotBefore,
                expires: jwtOptions.Expiration,
                signingCredentials: jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(ConfigConstants.Jwt.Id, id),
                new Claim(ConfigConstants.Jwt.Rol, ConfigConstants.Jwt.ApiAccess)
            });
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            var offset = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var unixDate = (long)Math.Round((date.ToUniversalTime() - offset).TotalSeconds);

            return unixDate;
        }
    }
}
