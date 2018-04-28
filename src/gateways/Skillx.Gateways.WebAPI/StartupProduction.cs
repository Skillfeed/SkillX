using Microsoft.Extensions.Configuration;

namespace Skillx.Gateways.WebAPI
{
    public class StartupProduction : BaseStartup
    {
        public StartupProduction(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
