using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using U.Dapper;
using U.Dapper.Startup.Configuration;
using U.Dapper;
using U.Dapper.Sql;

namespace U.Tests.Dapper.Repositories
{


    [TestClass]
    public class UserRepositoryTests
    {
        private void IocInitilize()
        {
            UStarterHelperTests.Start();
            UPrimeEngine.Instance.Register<IDapperConfiguration, TestDapperConfiguration>();
            UPrimeEngine.Instance.Register<IDapperContextProvider, DapperContextProvider>();
        }

        [TestMethod]
        public void SqlHelper2()
        {
            IocInitilize();
            //var list = U.Dapper.SqlHelper.GetDataTable("select * from Users");
            //var b = list.Rows.Count;
            var a = 1;
        }

        [TestMethod]
        public void UserBatchInsert_GetIds()
        {

            IocInitilize();

            var userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            userRepository.Log();
            //User user = new User();
            //user.Username = "15800448791";
            //user.Password = "123456";
            //user.RealName = "haha2";
            //userRepository.Insert(user);
            //DateTime start = DateTime.Now;
            //List<int> ids = new List<int>();
            //for (int i = 0; i < 1000; i++)
            //{
            //    User user2 = new User();
            //    user2.Username = "15800448791";
            //    user2.Password = "123456";
            //    user2.RealName = "haha2";
            //    userRepository.Insert(user2);
            //    ids.Add(user2.Id);
            //}

            //double total = DateTime.Now.Subtract(start).TotalMilliseconds;
            //Console.WriteLine("Total Time:" + total);
            //Console.WriteLine("Average Time:" + total / 1000);
        }

        [TestMethod]
        public void UserInsert()
        {

            IocInitilize();
            DateTime start = DateTime.Now;
            var userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();
            Console.WriteLine("Resolve UserRepository Total Time:" + (DateTime.Now - start).TotalMilliseconds);
            User user = new User();
            user.Username = "15800448791";
            user.Password = "123456";
            user.RealName = "haha2";

            userRepository.Insert(user);
            Console.WriteLine("Insert Total Time:" + (DateTime.Now - start).TotalMilliseconds);
        }

        [TestMethod]
        public void UserBatchInsert()
        {


            IocInitilize();
            var userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();
            var list = new List<User>();
            for (int i = 0; i < 50; i++)
            {
                User user = new User();
                user.Username = "15800448791" + i;
                user.Password = "123456" + i;
                user.RealName = "realName" + i;
                user.RoleId = 1;
                user.CreationTime = DateTime.Now;
                user.CreatorUserId = 0;
                list.Add(user);
            }
            //userRepository.Insert(list);
            //userRepository.Database.RunInTransaction(new Action(()=>{
            //    userRepository.Database.Insert(list);
            //    }));
            //userRepository.Database.Insert()

            //using (var connection = userRepository.Connection)
            //{
            //    using (var trans = connection.BeginTransaction())
            //    {
            //        try
            //        {
            //            connection.Execute(sql, list, trans, 30);
            //        }
            //        catch {
            //            trans.Rollback();
            //        }
            //    }
            //}
        }

        [TestMethod]
        public void User_Update()
        {

            IocInitilize();

            var userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            User user = new User();
            user.Username = "15800448791";
            user.Password = "123456";
            user.RealName = "haha2";

            userRepository.Insert(user);
            Console.WriteLine("inser userId：" + user.Id);

            var user2 = userRepository.Get(user.Id);
            user2.Username = "13626686806";
            user2.RoleId = 2;
            user2.Password = "123";
            user2.RealName = "haha";
            user2.CreationTime = DateTime.Now;
            userRepository.Update(user2);
        }

        [TestMethod]
        public void User_Delete()
        {

            IocInitilize();

            var userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            User user = new User();
            user.Username = "15800448791";
            user.Password = "123456";
            user.RealName = "haha2";
            userRepository.Insert(user);

            var user2 = userRepository.Get(user.Id);
            //userRepository.DeleteAsync(user2);
            Console.WriteLine(user2.Id);
            //Assert.IsNull(user2);
        }


        [TestMethod]
        public void Users_Get()
        {
            IocInitilize();

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var user = userRepository.Get(13621);
            Console.WriteLine(user.SerializeJson<User>());

        }

        [TestMethod]
        public void Users_Get_MultiQuery1()
        {
            IocInitilize();

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var userInfo = userRepository.Get<UserRole>(13621, UserQueryMapper.Map_Role, (user, role) =>
            {
                user.Role = role;
                return user;
            });
            Console.WriteLine(userInfo.SerializeJson<User>());
        }

        [TestMethod]
        public void Users_Get_MultiQuery2()
        {
            IocInitilize();

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var userInfo = userRepository.Get<UserRole, Area>(13621, UserQueryMapper.Map_Role_Area, (user, role, area) =>
            {
                user.Role = role;
                user.Province = area;
                return user;
            });
            Console.WriteLine(userInfo.SerializeJson<User>());
        }

        [TestMethod]
        public void Users_GetList()
        {
            IocInitilize();

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var list = userRepository.GetList();
            Assert.AreEqual(15, list.Count());

        }

        [TestMethod]
        public void Users_GetListMultiQuery1()
        {
            IocInitilize();

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var list = userRepository.GetList<UserRole>(UserQueryMapper.Map_Role, (user, role) => { user.Role = role; return user; });
            Assert.AreEqual(15, list.Count());

        }

        [TestMethod]
        public void Users_GetListMultiQuery2()
        {
            IocInitilize();

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var list = userRepository.GetList<UserRole, Area>(UserQueryMapper.Map_Role_Area, (user, role, area) => { user.Role = role; user.Province = area; return user; });
            Assert.AreEqual(15, list.Count());

        }

        [TestMethod]
        public void Users_GetPage()
        {
            IocInitilize();

            IList<ISort> sort = new List<ISort>(){
                Predicates.Sort<User>(x=> x.CreationTime)
            };

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var list = userRepository.GetPage(0, 10, null, sort);
            Assert.AreEqual(10, list.Count());

        }

        [TestMethod]
        public void Users_GetPageMultiQuery1()
        {
            IocInitilize();
            IList<ISort> sort = new List<ISort>(){
                Predicates.Sort<User>(x=> x.CreationTime)
            };

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var list = userRepository.GetPage<UserRole>(UserQueryMapper.Map_Role, (user, role) => { user.Role = role; return user; }, 0, 10, null, sort);
            Assert.AreEqual(10, list.Count());

        }

        [TestMethod]
        public void Users_GetPageMultiQuery2()
        {
            IocInitilize();
            IList<ISort> sort = new List<ISort>(){
                Predicates.Sort<User>(x=> x.CreationTime)
            };

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var list = userRepository.GetPage<UserRole, Area>(UserQueryMapper.Map_Role_Area, (user, role, area) => { user.Role = role; user.Province = area; return user; }, 0, 10, null, sort);
            Assert.AreEqual(10, list.Count());

        }

        [TestMethod]
        public void Users_Count() {
            IocInitilize();

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var list = userRepository.Count();
            Assert.AreEqual(15, list);
        }

        [TestMethod]
        public void Users_CountMultiQuery1()
        {
            IocInitilize();

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var count = userRepository.Count(UserQueryMapper.Map_Role);
            Assert.AreEqual(15, count);
        }

        [TestMethod]
        public void Users_CountMultiQuery2()
        {
            IocInitilize();

            UserRepository userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();

            var count = userRepository.Count(UserQueryMapper.Map_Role_Area);
            Assert.AreEqual(15, count);
        }

    }
}
