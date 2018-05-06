using System.Collections.Generic;

namespace Skillx.Modules.Courses.Core.Entities
{
    public class Course
    {
        public object Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<Author> Authors { get; set; }

        public IEnumerable<Video> Videos { get; set; }
    }
}
