using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Skillx.Modules.User.Core.Options;
using Skillx.Modules.User.Core.Repositories;
using System.Threading.Tasks;

namespace Skillx.Modules.User.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<Core.Entities.User> collection;

        public UserRepository(IOptions<DatabaseOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);

            this.db = client.GetDatabase(options.Value.DatabaseName);
            this.collection = this.db.GetCollection<Core.Entities.User>("users");
        }

        public async Task AddUserAsync(Core.Entities.User user)
        {
            await collection.InsertOneAsync(user);
        }
    }
}
