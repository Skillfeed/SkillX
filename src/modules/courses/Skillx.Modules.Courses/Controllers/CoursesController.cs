using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skillx.Modules.Courses.Core;
using Skillx.Modules.Courses.Models;
using System.Threading.Tasks;

namespace Skillx.Modules.Courses.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICourseManager courseManager;
        private readonly IMapper mapper;

        public CoursesController(ICourseManager courseManager, IMapper mapper)
        {
            this.courseManager = courseManager;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            var entity = this.mapper.Map<Core.Entities.Course>(course);

            await this.courseManager.CreateAsync(entity);

            var response = this.CreateDefaultResponse(success: true, data: entity);
            return this.Ok(response);
        }
    }
}
