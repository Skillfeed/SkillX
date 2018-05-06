using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Skillx.Modules.Courses.Core.Entities;
using Skillx.Modules.Courses.Core.Options;
using Skillx.Modules.Courses.Core.Repositories;

namespace Skillx.Modules.Courses.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<Course> collection;

        public CourseRepository(IOptions<DatabaseOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);

            this.db = client.GetDatabase(options.Value.DatabaseName);
            this.collection = this.db.GetCollection<Course>("courses");
        }

        public async Task AddCourseAsync(Course course)
        {
            await this.collection.InsertOneAsync(course);
        }
    }
}
