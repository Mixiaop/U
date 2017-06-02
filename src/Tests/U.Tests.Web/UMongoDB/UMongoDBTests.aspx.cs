using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;
using U.Domain.Entities;
using U.Domain.Entities.Auditing;
using U.MongoDB;
using U.MongoDB.Repositories;

namespace U.Tests.Web.UMongoDB
{
    public partial class UMongoDBTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var client = new MongoClient();
            //var database = client.GetDatabase("");
            //IMongoDatabaseProvider databaseProvider = UPrimeEngine.Instance.Resolve<IMongoDatabaseProvider>();

            //var list = databaseProvider.Database.ListCollections();
            //var a = 1;

            IUserRepository userRepository = UPrimeEngine.Instance.Resolve<IUserRepository>();
            User user = new User();
            user.Username = "heyben";
            user.Password = "123123";
            userRepository.Insert(user);

            Response.Write("success");

        }
    }

    public class User : Entity
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public interface IUserRepository : U.Domain.Repositories.IRepository<User> { }
    public class UserRepository : MongoDbRepositoryBase<User>, IUserRepository
    {
        public UserRepository(IMongoDatabaseProvider databaseProvider): base(databaseProvider) { }
    }
}