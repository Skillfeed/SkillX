using Microsoft.Extensions.Configuration;

namespace Skillx.Modules.Courses
{
    public class StartupProduction : BaseStartup
    {
        public StartupProduction(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
