using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace U.Tests.Extensions
{
    [TestClass]
    public class CollectionTests
    {
        public IList<User> GetData()
        {
            IList<User> list = new List<User> { 
                new User{ Id=1,Username="15800448791", Role=new UserRole(){ Id=1,RoleName="Admin"}},
                new User{ Id=2,Username="15800448792", Role=new UserRole(){ Id=1,RoleName="Admin"}},
                new User{ Id=3,Username="15800448793", Role=new UserRole(){ Id=1,RoleName="General"}},
                new User{ Id=4,Username="15800448794", Role=new UserRole(){ Id=1,RoleName="Admin"}},
                new User{ Id=5,Username="15800448795", Role=new UserRole(){ Id=1,RoleName="Admin"}}
            };
            return list;
        }

        [TestMethod]
        public void ToStringArray()
        {
            var list = GetData();

            string[] result = list.ToArray<User>(x => x.Username);
            Console.Write(result.SerializeJson());
        }

        [TestMethod]
        public void ToIntArray()
        {
            var list = GetData();

            int[] result = list.ToArray<User>(x => x.Id);
            Console.Write(result.SerializeJson());
        }

        [TestMethod]
        public void ToTypeArray()
        {
            var list = GetData();

            IList<UserRole> result = list.ToArray<User, UserRole>(x => x.Role);
            Console.Write(result.SerializeJson());
        }


        [TestMethod]
        public void ToStringArrayToString()
        {
            var list = GetData();

            string[] result = list.ToArray<User>(x => x.Username);

            var str = result.ToFormatString();
            Console.Write(str);
        }

        [TestMethod]
        public void ToIntArrayToString()
        {
            var list = GetData();

            int[] result = list.ToArray<User>(x => x.Id);
            var str = result.ToFormatString();
            Console.Write(str);
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public UserRole Role { get; set; }
    }

    public class UserRole
    {
        public int Id { get; set; }

        public string RoleName { get; set; }
    }
}
