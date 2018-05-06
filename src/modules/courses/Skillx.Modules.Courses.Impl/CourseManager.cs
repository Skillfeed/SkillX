using System.Threading.Tasks;
using Skillx.Modules.Courses.Core;
using Skillx.Modules.Courses.Core.Entities;
using Skillx.Modules.Courses.Core.Repositories;

namespace Skillx.Modules.Courses.Impl
{
    public class CourseManager : ICourseManager
    {
        private readonly ICourseRepository courses;

        public CourseManager(ICourseRepository courses)
        {
            this.courses = courses;
        }

        public async Task CreateAsync(Course course)
        {
            await this.courses.AddCourseAsync(course);
        }
    }
}
