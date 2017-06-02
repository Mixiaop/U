using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using Dapper;
using U.Dapper.Mapper;
using U.Dapper.Sql;

namespace U.Dapper
{
    public interface IDapperImplementor
    {
        ISqlGenerator SqlGenerator { get; }
        T Get<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;
        T Get<T, T2>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;
        T Get<T, T2>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;
        T Get<T, T2, T3>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;
        T Get<T, T2, T3>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;
        T Get<T, T2, T3, T4>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;
        T Get<T, T2, T3, T4>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;
        T Get<T, T2, T3, T4, T5>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;
        T Get<T, T2, T3, T4, T5>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;

        IEnumerable<T> GetList<T>(IDbConnection connection, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetList<T, T2>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetList<T, T2>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetList<T, T2, T3>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetList<T, T2, T3>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetList<T, T2, T3, T4>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetList<T, T2, T3, T4>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetList<T, T2, T3, T4, T5>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetList<T, T2, T3, T4, T5>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;

        IEnumerable<T> GetPage<T>(IDbConnection connection, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetPage<T, T2>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetPage<T, T2>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetPage<T, T2, T3>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetPage<T, T2, T3>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetPage<T, T2, T3, T4>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetPage<T, T2, T3, T4>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetPage<T, T2, T3, T4, T5>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        IEnumerable<T> GetPage<T, T2, T3, T4, T5>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;

        int Count<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class;
        int Count<T>(IDbConnection connection, Func<IClassMapper> funcMapInput, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class;
        int Count<T>(IDbConnection connection, IClassMapper mapInput, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class;

        void Insert<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, int? commandTimeout) where T : class;
        dynamic Insert<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class;
        bool Update<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class;
        bool Delete<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class;
        bool Delete<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class;
    }

    public class DapperImplementor : IDapperImplementor
    {
        #region Fileds & Ctor
        public DapperImplementor(ISqlGenerator sqlGenerator)
        {
            SqlGenerator = sqlGenerator;
        }

        public ISqlGenerator SqlGenerator { get; private set; }
        #endregion

        #region Get
        public T Get<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate predicate = GetIdPredicate(classMap, id);
            var command = BuildQueryCommand(classMap, predicate, null);
            T result = connection.Query<T>(command.Sql, command.Parameters, transaction, true, commandTimeout, CommandType.Text).SingleOrDefault();

            return result;
        }
        public T Get<T, T2>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return Get<T, T2>(connection, funcMapInput(), funcMapOutput, id, transaction, commandTimeout);
        }

        public T Get<T, T2>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IPredicate predicate = GetIdPredicate(mapInput, id);
            var command = BuildQueryCommand(mapInput, predicate, null);

            T result = connection.Query<T, T2, T>(command.Sql, funcMapOutput, command.Parameters, transaction, true, command.SplitOn, commandTimeout, CommandType.Text).SingleOrDefault();
            return result;
        }

        public T Get<T, T2, T3>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return Get<T, T2, T3>(connection, funcMapInput(), funcMapOutput, id, transaction, commandTimeout);
        }
        public T Get<T, T2, T3>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IPredicate predicate = GetIdPredicate(mapInput, id);
            var command = BuildQueryCommand(mapInput, predicate, null);

            return connection.Query<T, T2, T3, T>(command.Sql, funcMapOutput, command.Parameters, transaction, true, command.SplitOn, commandTimeout, CommandType.Text).SingleOrDefault();
        }

        public T Get<T, T2, T3, T4>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return Get<T, T2, T3, T4>(connection, funcMapInput(), funcMapOutput, id, transaction, commandTimeout);
        }
        public T Get<T, T2, T3, T4>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IPredicate predicate = GetIdPredicate(mapInput, id);
            var command = BuildQueryCommand(mapInput, predicate, null);

            return connection.Query<T, T2, T3, T4, T>(command.Sql, funcMapOutput, command.Parameters, transaction, true, command.SplitOn, commandTimeout, CommandType.Text).SingleOrDefault();
        }

        public T Get<T, T2, T3, T4, T5>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return Get<T, T2, T3, T4, T5>(connection, funcMapInput(), funcMapOutput, id, transaction, commandTimeout);
        }
        public T Get<T, T2, T3, T4, T5>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IPredicate predicate = GetIdPredicate(mapInput, id);
            var command = BuildQueryCommand(mapInput, predicate, null);

            return connection.Query<T, T2, T3, T4, T5, T>(command.Sql, funcMapOutput, command.Parameters, transaction, true, command.SplitOn, commandTimeout, CommandType.Text).SingleOrDefault();
        }
        #endregion

        #region GetList
        public IEnumerable<T> GetList<T>(IDbConnection connection, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            var command = BuildQueryCommand(classMap, predicate, null);
            return connection.Query<T>(command.Sql, command.Parameters, transaction, buffered, commandTimeout, CommandType.Text);
            //try {

            //}
            //catch (SqlException ex)
            //{
            //    throw new UDapperException("nima", ex);
            //}
        }

        public IEnumerable<T> GetList<T, T2>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return GetList<T, T2>(connection, funcMapInput(), funcMapOutput, predicate, sort, transaction, commandTimeout, buffered);
        }

        public IEnumerable<T> GetList<T, T2>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var command = BuildQueryCommand(mapInput, predicate, sort);
            return connection.Query<T, T2, T>(command.Sql, funcMapOutput, command.Parameters, transaction, buffered, command.SplitOn, commandTimeout, CommandType.Text);
        }

        public IEnumerable<T> GetList<T, T2, T3>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return GetList<T, T2, T3>(connection, funcMapInput(), funcMapOutput, predicate, sort, transaction, commandTimeout, buffered);
        }

        public IEnumerable<T> GetList<T, T2, T3>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var command = BuildQueryCommand(mapInput, predicate, sort);
            return connection.Query<T, T2, T3, T>(command.Sql, funcMapOutput, command.Parameters, transaction, buffered, command.SplitOn, commandTimeout, CommandType.Text);
        }

        public IEnumerable<T> GetList<T, T2, T3, T4>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return GetList<T, T2, T3, T4>(connection, funcMapInput(), funcMapOutput, predicate, sort, transaction, commandTimeout, buffered);
        }
        public IEnumerable<T> GetList<T, T2, T3, T4>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var command = BuildQueryCommand(mapInput, predicate, sort);
            return connection.Query<T, T2, T3, T4, T>(command.Sql, funcMapOutput, command.Parameters, transaction, buffered, command.SplitOn, commandTimeout, CommandType.Text);
        }

        public IEnumerable<T> GetList<T, T2, T3, T4, T5>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return GetList<T, T2, T3, T4, T5>(connection, funcMapInput(), funcMapOutput, predicate, sort, transaction, commandTimeout, buffered);
        }
        public IEnumerable<T> GetList<T, T2, T3, T4, T5>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var command = BuildQueryCommand(mapInput, predicate, sort);
            return connection.Query<T, T2, T3, T4, T5, T>(command.Sql, funcMapOutput, command.Parameters, transaction, buffered, command.SplitOn, commandTimeout, CommandType.Text);
        }
        #endregion

        #region GetPage
        public IEnumerable<T> GetPage<T>(IDbConnection connection, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            var command = BuildQueryCommand(classMap, predicate, sort, DapperCommandType.SelectPage, page, resultsPerPage);

            return connection.Query<T>(command.Sql, command.Parameters, transaction, buffered, commandTimeout, CommandType.Text);
        }
        public IEnumerable<T> GetPage<T, T2>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return GetPage<T, T2>(connection, funcMapInput(), funcMapOutput, predicate, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }

        public IEnumerable<T> GetPage<T, T2>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var command = BuildQueryCommand(mapInput, predicate, sort, DapperCommandType.SelectPage, page, resultsPerPage);

            return connection.Query<T, T2, T>(command.Sql, funcMapOutput, command.Parameters, transaction, buffered, command.SplitOn, commandTimeout, CommandType.Text);
        }

        public IEnumerable<T> GetPage<T, T2, T3>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return GetPage<T, T2, T3>(connection, funcMapInput(), funcMapOutput, predicate, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }

        public IEnumerable<T> GetPage<T, T2, T3>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var command = BuildQueryCommand(mapInput, predicate, sort, DapperCommandType.SelectPage, page, resultsPerPage);

            return connection.Query<T, T2, T3, T>(command.Sql, funcMapOutput, command.Parameters, transaction, buffered, command.SplitOn, commandTimeout, CommandType.Text);
        }

        public IEnumerable<T> GetPage<T, T2, T3, T4>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return GetPage<T, T2, T3, T4>(connection, funcMapInput(), funcMapOutput, predicate, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }
        public IEnumerable<T> GetPage<T, T2, T3, T4>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var command = BuildQueryCommand(mapInput, predicate, sort, DapperCommandType.SelectPage, page, resultsPerPage);

            return connection.Query<T, T2, T3, T4, T>(command.Sql, funcMapOutput, command.Parameters, transaction, buffered, command.SplitOn, commandTimeout, CommandType.Text);
        }
        public IEnumerable<T> GetPage<T, T2, T3, T4, T5>(IDbConnection connection, Func<IClassMapper> funcMapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return GetPage<T, T2, T3, T4, T5>(connection, funcMapInput(), funcMapOutput, predicate, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }
        public IEnumerable<T> GetPage<T, T2, T3, T4, T5>(IDbConnection connection, IClassMapper mapInput, Func<T, T2, T3, T4, T5, T> funcMapOutput, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var command = BuildQueryCommand(mapInput, predicate, sort, DapperCommandType.SelectPage, page, resultsPerPage);

            return connection.Query<T, T2, T3, T4, T5, T>(command.Sql, funcMapOutput, command.Parameters, transaction, buffered, command.SplitOn, commandTimeout, CommandType.Text);
        }
        #endregion

        #region Count
        public int Count<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            var command = BuildQueryCommand(classMap, predicate, null, DapperCommandType.Count);

            return (int)connection.Query(command.Sql, command.Parameters, transaction, false, commandTimeout, CommandType.Text).Single().Total;
        }

        public int Count<T>(IDbConnection connection, Func<IClassMapper> mapInput, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return Count<T>(connection, mapInput(), predicate, transaction, commandTimeout);
        }

        public int Count<T>(IDbConnection connection, IClassMapper mapInput, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            var command = BuildQueryCommand(mapInput, predicate, null, DapperCommandType.Count);

            return (int)connection.Query(command.Sql, command.Parameters, transaction, false, commandTimeout, CommandType.Text).Single().Total;
        }
        #endregion

        #region CUD
        public void Insert<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            var properties = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);

            foreach (var e in entities)
            {
                foreach (var column in properties)
                {
                    if (column.KeyType == KeyType.Guid)
                    {
                        Guid comb = SqlGenerator.Configuration.GetNextGuid();
                        column.PropertyInfo.SetValue(e, comb, null);
                    }
                }
            }

            string sql = SqlGenerator.Insert(classMap);

            connection.Execute(sql, entities, transaction, commandTimeout, CommandType.Text);
        }

        public dynamic Insert<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            List<IPropertyMap> nonIdentityKeyProperties = classMap.Properties.Where(p => p.KeyType == KeyType.Guid || p.KeyType == KeyType.Assigned).ToList();
            var identityColumn = classMap.Properties.SingleOrDefault(p => p.KeyType == KeyType.Identity);
            foreach (var column in nonIdentityKeyProperties)
            {
                if (column.KeyType == KeyType.Guid)
                {
                    Guid comb = SqlGenerator.Configuration.GetNextGuid();
                    column.PropertyInfo.SetValue(entity, comb, null);
                }
            }

            IDictionary<string, object> keyValues = new ExpandoObject();
            string sql = SqlGenerator.Insert(classMap);
            if (identityColumn != null)
            {
                IEnumerable<long> result;
                if (SqlGenerator.SupportsMultipleStatements())
                {
                    sql += SqlGenerator.Configuration.Dialect.BatchSeperator + SqlGenerator.IdentitySql(classMap);
                    result = connection.Query<long>(sql, entity, transaction, false, commandTimeout, CommandType.Text);
                }
                else
                {
                    connection.Execute(sql, entity, transaction, commandTimeout, CommandType.Text);
                    sql = SqlGenerator.IdentitySql(classMap);
                    result = connection.Query<long>(sql, entity, transaction, false, commandTimeout, CommandType.Text);
                }

                long identityValue = result.First();
                int identityInt = Convert.ToInt32(identityValue);
                keyValues.Add(identityColumn.Name, identityInt);
                identityColumn.PropertyInfo.SetValue(entity, identityInt, null);
            }
            else
            {
                connection.Execute(sql, entity, transaction, commandTimeout, CommandType.Text);
            }

            foreach (var column in nonIdentityKeyProperties)
            {
                keyValues.Add(column.Name, column.PropertyInfo.GetValue(entity, null));
            }

            if (keyValues.Count == 1)
            {
                return keyValues.First().Value;
            }

            return keyValues;
        }

        public bool Update<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate predicate = GetKeyPredicate<T>(classMap, entity);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Update(classMap, predicate, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();

            var columns = classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity));
            foreach (var property in ReflectionHelper.GetObjectValues(entity).Where(property => columns.Any(c => c.Name == property.Key)))
            {
                dynamicParameters.Add(property.Key, property.Value);
            }

            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Execute(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text) > 0;
        }

        public bool Delete<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate predicate = GetKeyPredicate<T>(classMap, entity);
            return Delete<T>(connection, classMap, predicate, transaction, commandTimeout);
        }

        public bool Delete<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return Delete<T>(connection, classMap, wherePredicate, transaction, commandTimeout);
        }
        #endregion

        #region Protected Methods
        protected bool Delete<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Delete(classMap, predicate, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Execute(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text) > 0;
        }

        protected IPredicate GetPredicate(IClassMapper classMap, object predicate)
        {
            IPredicate wherePredicate = predicate as IPredicate;
            if (wherePredicate == null && predicate != null)
            {
                wherePredicate = GetEntityPredicate(classMap, predicate);
            }

            return wherePredicate;
        }

        protected IPredicate GetIdPredicate(IClassMapper classMap, object id)
        {
            bool isSimpleType = ReflectionHelper.IsSimpleType(id.GetType());
            var keys = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);
            IDictionary<string, object> paramValues = null;
            IList<IPredicate> predicates = new List<IPredicate>();
            if (!isSimpleType)
            {
                paramValues = ReflectionHelper.GetObjectValues(id);
            }

            foreach (var key in keys)
            {
                object value = id;
                if (!isSimpleType)
                {
                    value = paramValues[key.Name];
                }

                Type predicateType = typeof(FieldPredicate<>).MakeGenericType(classMap.EntityType);

                IFieldPredicate fieldPredicate = Activator.CreateInstance(predicateType) as IFieldPredicate;
                fieldPredicate.Not = false;
                fieldPredicate.Operator = Operator.Eq;
                fieldPredicate.PropertyName = key.Name;
                fieldPredicate.Value = value;
                predicates.Add(fieldPredicate);
            }

            return predicates.Count == 1
                       ? predicates[0]
                       : new PredicateGroup
                             {
                                 Operator = GroupOperator.And,
                                 Predicates = predicates
                             };
        }

        protected IPredicate GetKeyPredicate<T>(IClassMapper classMap, T entity) where T : class
        {
            var whereFields = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);
            if (!whereFields.Any())
            {
                throw new ArgumentException("At least one Key column must be defined.");
            }

            IList<IPredicate> predicates = (from field in whereFields
                                            select new FieldPredicate<T>
                                                       {
                                                           Not = false,
                                                           Operator = Operator.Eq,
                                                           PropertyName = field.Name,
                                                           Value = field.PropertyInfo.GetValue(entity, null)
                                                       }).Cast<IPredicate>().ToList();

            return predicates.Count == 1
                       ? predicates[0]
                       : new PredicateGroup
                             {
                                 Operator = GroupOperator.And,
                                 Predicates = predicates
                             };
        }

        protected IPredicate GetEntityPredicate(IClassMapper classMap, object entity)
        {
            Type predicateType = typeof(FieldPredicate<>).MakeGenericType(classMap.EntityType);
            IList<IPredicate> predicates = new List<IPredicate>();
            foreach (var kvp in ReflectionHelper.GetObjectValues(entity))
            {
                IFieldPredicate fieldPredicate = Activator.CreateInstance(predicateType) as IFieldPredicate;
                fieldPredicate.Not = false;
                fieldPredicate.Operator = Operator.Eq;
                fieldPredicate.PropertyName = kvp.Key;
                fieldPredicate.Value = kvp.Value;
                predicates.Add(fieldPredicate);
            }

            return predicates.Count == 1
                       ? predicates[0]
                       : new PredicateGroup
                       {
                           Operator = GroupOperator.And,
                           Predicates = predicates
                       };
        }

        protected DapperCommand BuildQueryCommand(IClassMapper classMap, object predicate, IList<ISort> sort, DapperCommandType commandType = DapperCommandType.Select, int pageIndex = 1, int pageSize = 20)
        {
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return BuildQueryCommand(classMap, wherePredicate, sort, commandType, pageIndex, pageSize);
        }

        protected DapperCommand BuildQueryCommand(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, DapperCommandType commandType = DapperCommandType.Select, int pageIndex = 1, int pageSize = 20)
        {
            DapperCommand command = new DapperCommand();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            switch (commandType)
            {
                case DapperCommandType.Select:
                    command.Sql = SqlGenerator.Select(classMap, predicate, sort, parameters);
                    break;
                case DapperCommandType.SelectPage:
                    command.Sql = SqlGenerator.SelectPaged(classMap, predicate, sort, pageIndex, pageSize, parameters);
                    break;
                case DapperCommandType.Count:
                    command.Sql = SqlGenerator.Count(classMap, predicate, parameters);
                    break;
            }

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter.Key, parameter.Value);
            }
            command.SplitOn = classMap.MultiQuerySplitOn;
            return command;

        }
        #endregion

        #region Inner class
        public enum DapperCommandType
        {
            Select,
            SelectPage,
            Count
        }
        public class DapperCommand
        {
            DynamicParameters _dynamicParameters = new DynamicParameters();

            public string Sql { get; set; }

            public DynamicParameters Parameters
            {
                get
                {
                    return _dynamicParameters;
                }
            }

            public string SplitOn { get; set; }

        }
        #endregion
    }

}
