using System.Collections.Generic;

namespace Skillx.Gateways.WebAPI.Models.Course
{
    public class CreateCourseRequestModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<CourseAuthor> Authors { get; set; }
    }
}
