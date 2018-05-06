using System.Collections.Generic;

namespace Skillx.Modules.Courses.Models
{
    public class Course
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<Author> Authors { get; set; }

        public IEnumerable<Video> Videos { get; set; }
    }
}
