using Skillx.Modules.Courses.Core.Entities;
using System.Threading.Tasks;

namespace Skillx.Modules.Courses.Core.Repositories
{
    public interface ICourseRepository
    {
        Task AddCourseAsync(Course course);
    }
}
