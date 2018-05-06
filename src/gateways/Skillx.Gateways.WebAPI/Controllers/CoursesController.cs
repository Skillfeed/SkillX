using Microsoft.AspNetCore.Mvc;
using Skillx.Gateways.WebAPI.Models.Course;
using Skillx.Gateways.WebAPI.Services.Abstraction;
using System.Threading.Tasks;

namespace Skillx.Gateways.WebAPI.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseRequestModel courseRequest)
        {
            var response = await this.courseService.Create(courseRequest);

            return this.GetActionResult(response);
        }
    }
}
