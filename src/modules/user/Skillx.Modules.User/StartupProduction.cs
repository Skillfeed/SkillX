using Microsoft.Extensions.Configuration;

namespace Skillx.Modules.User
{
    public class StartupProduction : BaseStartup
    {
        public StartupProduction(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
