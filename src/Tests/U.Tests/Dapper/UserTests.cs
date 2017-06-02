using U.Logging;
using U.Dapper;
using U.Dapper.Repositories;
using U.Dapper.Mapper;
using U.Domain.Entities;
using U.Domain.Entities.Auditing;

namespace U.Tests.Dapper
{
    public class DbConstsTests
    {
        public const string MultiQueryIgnoreColumns = "Id,IsDeleted,DeletionTime,DeleterUserId,LastModificationTime,LastModifierUserId,CreatorUserId,CreationTime";
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

    public class UserRole : FullAuditedEntity
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

    #region QueryMappers
    public class UserQueryMapper
    {
        public static UserMapper Map_Role()
        {
            var userMap = new UserMapper();
            var roleMap = new UserRoleMapper();
            userMap.SetMultiQueryIgnoreColumns("Role", "RoleName");
            roleMap.SetMultiQueryIgnoreColumns(DbConstsTests.MultiQueryIgnoreColumns);
            userMap.MapLeftJoin(x => x.Role, "RoleId", roleMap, "Id");
            return userMap;
        }

        public static UserMapper Map_Role_Area()
        {
            var userMap = new UserMapper();
            var roleMap = new UserRoleMapper();
            var areaMap = new AreaMapper();
            userMap.SetMultiQueryIgnoreColumns("Role,Province", "RoleName,AreaName");
            roleMap.SetMultiQueryIgnoreColumns(DbConstsTests.MultiQueryIgnoreColumns);
            areaMap.SetMultiQueryIgnoreColumns(DbConstsTests.MultiQueryIgnoreColumns);

            userMap.MapLeftJoin(x => x.Role, "RoleId", roleMap, "Id");
            userMap.MapLeftJoin(x => x.Province, "AreaId", areaMap, "Id");
            return userMap;
        } 
    }
    #endregion

    public class UserRepository : DapperRepositoryBase<User>
    {
        public ILogger Logger { get; set; }

        public UserRepository(IDapperContextProvider dbContext) : base(dbContext) {
            Logger = NullLogger.Instance;

            var a = 1;
        }

        public void Log() {
            Logger.Error("haha");
            var a = 1;
        }
    }

    #endregion
}
