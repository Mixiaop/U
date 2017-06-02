using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using U.Dapper;
using U.Dapper.Startup.Configuration;
using U.Dapper;
using U.Dapper.Mapper;
using U.Dapper.Sql;
using U.Tests.Dapper.Repositories;

namespace U.Tests.Dapper.Extensions
{
    [TestClass]
    public class SqlGeneratorTests
    {
        public static ISqlGenerator Setup()
        {
            UStarterHelperTests.Start();
            UPrimeEngine.Instance.Register<IDapperConfiguration, TestDapperConfiguration>();
            UPrimeEngine.Instance.Register<IDapperContextProvider, DapperContextProvider>(Dependency.DependencyLifeStyle.Singleton);
            var config = UPrimeEngine.Instance.Resolve<IDapperConfiguration>();
            var sqlGenerator = new SqlGeneratorImpl(config);
            return sqlGenerator;
        }

        public static ISqlGenerator GetSqlGenerator()
        {
            var config = UPrimeEngine.Instance.Resolve<IDapperConfiguration>();
            var sqlGenerator = new SqlGeneratorImpl(config);
            return sqlGenerator;
        }

        [TestMethod]
        public void Mappers_Insert()
        {

            ISqlGenerator sqlGenerator = Setup();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var sql = sqlGenerator.Insert(new UserMapper());
            Console.WriteLine(sql);
            //ISqlGenerator generator = new SqlGeneratorImpl()
        }

        [TestMethod]
        public void Mappers_GetList()
        {
            ISqlGenerator sqlGenerator = Setup();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            Func<IClassMapper> map;

            map = (() =>
            {
                var userMap = new UserMapper();
                userMap.MapLeftJoin(x => x.Role, "RoleId", new UserRoleMapper(), "Id");
                return userMap;
            });

            //Func<User, Area, User> outputMap;
            //outputMap = ((user,area) =>
            //{
            //    user.Role = area;
            //    return user;
            //});

            string sql = sqlGenerator.Select(map(), null, null, parameters);
            Console.WriteLine(sql);
        }

        [TestMethod]
        public void Mappers_Select()
        {
            ISqlGenerator sqlGenerator = Setup();


            DateTime start = DateTime.Now;
            var userRepository = UPrimeEngine.Instance.Resolve<UserRepository>();


            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = sqlGenerator.Select(new UserMapper(), null, null, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }


            var users = userRepository.Context.GetConnection().Query(sql, new Func<User, Area, User>((t1, t2) =>
            {
                t1.Province = t2;
                return t1;
            }));

            Console.WriteLine(sql);
            double total = DateTime.Now.Subtract(start).TotalMilliseconds;
            Console.WriteLine("Total Time:" + (DateTime.Now - start).TotalMilliseconds);
        }

        [TestMethod]
        public void Mappers_SelectPaged()
        {
            ISqlGenerator sqlGenerator = Setup();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            IList<ISort> sort = new List<ISort>
                                    {
                                        Predicates.Sort<User>(p => p.Id)
                                    };

            var user = new UserMapper();
            user.MapLeftJoin(x => x.Role, "RoleId", new UserRoleMapper(), "Id");
            var sql = sqlGenerator.SelectPaged(user, null, sort, 0, 10, parameters);
            Console.WriteLine(sql);
        }
    }


}
