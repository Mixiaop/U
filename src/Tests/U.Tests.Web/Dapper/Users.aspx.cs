using System;
using U.Dapper;
using U.Dapper.Repositories;
using U.Dapper.Mapper;
using U.Domain.Entities;
using U.Domain.Entities.Auditing;


namespace U.Tests.Web.Dapper
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            var userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();
            Console.WriteLine("Resolve UserRepository Total Time:" + (DateTime.Now - start).TotalMilliseconds);
            User user = new User();
            user.Username = "15800448791";
            user.Password = "123456";
            user.RealName = "haha2";

            userRepository.Insert(user);

            Response.Write("Insert Total Time:" + (DateTime.Now - start).TotalMilliseconds);
        }
    }
    #region Users
    public class User : FullAuditedEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string RealName { get; set; }

        public int RoleId { get; set; }

        public int AreaId { get; set; }

        public UserRole Role { get; set; }

        public Area Province { get; set; }
    }

    public class UserRole : Entity
    {
        public string RoleName { get; set; }
    }

    public class Area : Entity
    {
        public string AreaName { get; set; }
    }
    public class UserMapper : ClassMapper<User>
    {
        public UserMapper()
        {
            Table("Users");
            Map(x => x.Id).Key(KeyType.Identity);
            Map(x => x.Role).Ignore();
            Map(x => x.Province).Ignore();
            //MapLeftJoin(x => x.Role, new UserRoleMapper(), "Id", "RoleId");
            //MapLeftJoin(x => x.Province, new AreaMapper(), "Id", "AreaId");
            AutoMap();
        }
    }

    public class UserRoleMapper : ClassMapper<UserRole>
    {
        public UserRoleMapper()
        {
            Table("UserRoles");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }

    public class AreaMapper : ClassMapper<Area>
    {
        public AreaMapper()
        {
            Table("Areas");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }

    public class UserRepository : DapperRepositoryBase<User>
    {
        public UserRepository(IDapperContextProvider dbContext) : base(dbContext) { }
    }

    #endregion
   
}