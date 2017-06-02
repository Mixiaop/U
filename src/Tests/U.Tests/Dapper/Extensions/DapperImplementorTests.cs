using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using U.Dapper;
using U.Dapper.Sql;

namespace U.Tests.Dapper.Extensions
{
    [TestClass]
    public class DapperImplementorTests
    {
        [TestMethod]
        public void Get()
        {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));
            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var user = dapper.Get<User>(conn, 13621, null, null);
                conn.Close();
                Console.WriteLine(user.RealName);
            }
        }

        [TestMethod]
        public void Get_MultiQuery()
        {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));
            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var userInfo = dapper.Get<User, UserRole>(conn, UserQueryMapper.Map_Role, (user, role) =>
                {
                    user.Role = role;
                    return user;
                }, 13621, null, null);

                Console.WriteLine(userInfo.RealName);
                conn.Close();

            }
        }

        [TestMethod]
        public void Get_MultiQuery2()
        {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));
            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var userInfo = dapper.Get<User, UserRole, Area>(conn, UserQueryMapper.Map_Role_Area, (user, role, area) =>
                {
                    user.Role = role;
                    user.Province = area;
                    return user;
                }, 13621, null, null);

                Console.WriteLine(userInfo.RealName);
                conn.Close();

            }
        }


        [TestMethod]
        public void GetList()
        {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));

            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var list = dapper.GetList<User>(conn, null, null, null, null, false).ToList();
                conn.Close();

                Console.WriteLine(list.Count());
            }
        }

        [TestMethod]
        public void GetList_MultiQuery()
        {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));

            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var list = dapper.GetList<User, UserRole>(conn, UserQueryMapper.Map_Role, (user, role) =>
                {
                    user.Role = role;
                    return user;
                }, null, null, null, null, false).ToList();

                Console.WriteLine(list.Count());
                conn.Close();
            }
        }

        [TestMethod]
        public void GetList_MultiQuery2()
        {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));

            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var list = dapper.GetList<User, UserRole, Area>(conn, UserQueryMapper.Map_Role_Area, (user, role, area) =>
                {
                    user.Role = role;
                    user.Province = area;
                    return user;
                }, null, null, null, null, false).ToList();

                Console.WriteLine(list.Count());
                conn.Close();
            }
        }

        [TestMethod]
        public void GetPage()
        {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));
            IList<ISort> sort = new List<ISort>(){
                Predicates.Sort<User>(x=> x.CreationTime, false)
            };
            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var list = dapper.GetPage<User>(conn, null, sort, 0, 10, null, null, false).ToList();
                conn.Close();

                Assert.AreEqual(10, list.Count());
            }
        }

        [TestMethod]
        public void GetPage_MultiQuery()
        {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));
            IList<ISort> sort = new List<ISort>(){
                Predicates.Sort<User>(x=> x.CreationTime, false)
            };
            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var list = dapper.GetPage<User, UserRole>(conn, UserQueryMapper.Map_Role, (user, role) =>
                {
                    user.Role = role;
                    return user;
                }, null, sort, 31850, 10, null, null, false).ToList();
                conn.Close();

                Assert.AreEqual(10, list.Count());
            }
        }

        [TestMethod]
        public void GetPage_MultiQuery2()
        {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));
            IList<ISort> sort = new List<ISort>(){
                Predicates.Sort<User>(x=> x.CreationTime)
            };
            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var list = dapper.GetPage<User, UserRole, Area>(conn, UserQueryMapper.Map_Role_Area, (user, role, area) =>
                {
                    user.Role = role;
                    user.Province = area;
                    return user;
                }, null, sort, 0, 10, null, null, false).ToList();
                conn.Close();

                Assert.AreEqual(10, list.Count());
            }
        }

        [TestMethod]
        public void Count()
        {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));

            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var count = dapper.Count<User>(conn, null, null, null);
                conn.Close();

                Console.WriteLine(count);
            }
        }

        [TestMethod]
        public void Count_MultiQuery() {
            var config = new TestDapperConfiguration();
            IDapperImplementor dapper = new DapperImplementor(new SqlGeneratorImpl(config));

            using (SqlConnection conn = new SqlConnection(config.SqlConnectionString))
            {
                conn.Open();
                var count = dapper.Count<User>(conn, UserQueryMapper.Map_Role_Area, 
                    Predicates.Field<UserRole>(x=>x.RoleName, Operator.Eq,"管理员"),
                     null, null);
                conn.Close();

                Console.WriteLine(count);
            }
        }
    }
}
