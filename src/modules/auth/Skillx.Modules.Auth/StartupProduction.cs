using Microsoft.Extensions.Configuration;

namespace Skillx.Modules.Auth
{
    public class StartupProduction : BaseStartup
    {
        public StartupProduction(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
