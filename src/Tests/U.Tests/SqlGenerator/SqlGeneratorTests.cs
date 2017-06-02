using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using U.SqlGenerator;
using U.SqlGenerator.Attributes;

namespace U.Tests.SqlGenerator
{
    [TestClass]
    public class SqlGeneratorTests
    {
        [TestMethod]
        public void UsersInsertSql()
        {
            SqlGenerator<User> generator = new SqlGenerator<User>();
            User user = new User();
            user.Username = "15800448791";
            user.Password = "123456";
            user.RealName = "haha2";
            var sql = generator.GetInsert(user);

            Console.Write(sql.Sql);
        }

        [TestMethod]
        public void UsersUpdateSql()
        {
            SqlGenerator<User> generator = new SqlGenerator<User>();
            User user = new User();
            user.Id = 1;
            user.Username = "15800448791";
            user.Password = "123456";
            user.RealName = "haha2";
            var sql = generator.GetUpdate(user);

            Console.Write(sql.Sql);
        }

        [TestMethod]
        public void UsersSelectFirstSql()
        {
            SqlGenerator<User> generator = new SqlGenerator<User>();
            SqlQuery sql = generator.GetSelectFirst(x => x.Username == "heyben");

            Console.Write(sql.Sql);
        }

        [TestMethod]
        public void UsersSelectAllSql()
        {
            SqlGenerator<User> generator = new SqlGenerator<User>();
            SqlQuery sql = generator.GetSelectAll(x => x.RoleId == 1, p => p.UserRole);

            Console.WriteLine(sql.Sql);
            Console.WriteLine(sql.Param);
        }

        [TestMethod]
        public void UsersDeleteSql()
        {
            SqlGenerator<User> generator = new SqlGenerator<User>();
            User user = new User();
            user.Id = 1;
            user.Username = "15800448791";
            user.Password = "123456";
            user.RealName = "haha2";
            SqlQuery sql = generator.GetDelete(user);

            Console.WriteLine(sql.Sql);
        }

    }

    [Table("Users")]
    public class User
    {
        [Key, Identity]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string RealName { get; set; }


        public int RoleId { get; set; }

        [LeftJoin("UserRoles", "RoleId", "Id")]
        public UserRole UserRole { get; set; }

        public DateTime CreationTime { get; set; }
    }

    [Table("UserRoles")]
    public class UserRole
    {
        [Key, Identity]
        public int Id { get; set; }

        public string RoleName { get; set; }
    }
}
