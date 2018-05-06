using Skillx.Modules.Courses.Core.Entities;
using System.Threading.Tasks;

namespace Skillx.Modules.Courses.Core
{
    public interface ICourseManager
    {
        Task CreateAsync(Course course);
    }
}
