﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using U.DapperExtensions.Mapper;

namespace U.DapperExtensions.Sql
{
    public interface ISqlGenerator
    {
        IDapperExtensionsConfiguration Configuration { get; }

        string Select(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, IDictionary<string, object> parameters);
        string SelectPaged(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int page, int resultsPerPage, IDictionary<string, object> parameters);
        string SelectSet(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int firstResult, int maxResults, IDictionary<string, object> parameters);
        string Count(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters);

        string Insert(IClassMapper classMap);
        string Update(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters);
        string Delete(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters);

        string IdentitySql(IClassMapper classMap);
        string GetTableName(IClassMapper map);
        string GetColumnName(IClassMapper map, IPropertyMap property, bool includeAlias);
        string GetColumnName(IClassMapper map, string propertyName, bool includeAlias);
        bool SupportsMultipleStatements();
    }

    public class SqlGeneratorImpl : ISqlGenerator
    {
        public SqlGeneratorImpl(IDapperExtensionsConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IDapperExtensionsConfiguration Configuration { get; private set; }

        public virtual string Select(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, IDictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters");
            }

            string columns = "";
            if (classMap.HaveLeftJoinProperties)
            {
                columns = BuildSelectColumnsByLeftJoin(classMap);
            }
            else
            {
                columns = BuildSelectColumns(classMap);
            }

            StringBuilder sql = new StringBuilder(string.Format("SELECT {0} FROM {1}",
                columns,
                GetTableName(classMap)));

            if (classMap.HaveLeftJoinProperties)
            {
                sql.Append(BuildLeftJoinAndOn(classMap));
            }

            if (predicate != null)
            {
                sql.Append(" WHERE ")
                    .Append(predicate.GetSql(this, parameters));
            }

            if (sort != null && sort.Any())
            {
                sql.Append(" ORDER BY ")
                    .Append(sort.Select(s => GetColumnName(classMap, s.PropertyName, false) + (s.Ascending ? " ASC" : " DESC")).AppendStrings());
            }

            return sql.ToString();
        }

        public virtual string SelectPaged(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int page, int resultsPerPage, IDictionary<string, object> parameters)
        {
            if (sort == null || !sort.Any())
            {
                throw new ArgumentNullException("Sort", "Sort cannot be null or empty.");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters");
            }
            string columns = "";
            if (classMap.HaveLeftJoinProperties)
            {
                columns = BuildSelectColumnsByLeftJoin(classMap);
            }
            else
            {
                columns = BuildSelectColumns(classMap);
            }

            StringBuilder innerSql = new StringBuilder(string.Format("SELECT {0} FROM {1}",
                columns,
                GetTableName(classMap)));

            if (classMap.HaveLeftJoinProperties)
            {
                innerSql.Append(BuildLeftJoinAndOn(classMap));
            }
            if (predicate != null)
            {
                innerSql.Append(" WHERE ")
                    .Append(predicate.GetSql(this, parameters));
            }

            string orderBy = sort.Select(s => GetColumnName(classMap, s.PropertyName, false) + (s.Ascending ? " ASC" : " DESC")).AppendStrings();
            innerSql.Append(" ORDER BY " + orderBy);

            string sql = Configuration.Dialect.GetPagingSql(innerSql.ToString(), page, resultsPerPage, parameters);
            return sql;
        }

        public virtual string SelectSet(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int firstResult, int maxResults, IDictionary<string, object> parameters)
        {
            if (sort == null || !sort.Any())
            {
                throw new ArgumentNullException("Sort", "Sort cannot be null or empty.");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters");
            }

            string columns = "";
            if (classMap.HaveLeftJoinProperties)
            {
                columns = BuildSelectColumnsByLeftJoin(classMap);
            }
            else
            {
                columns = BuildSelectColumns(classMap);
            }

            StringBuilder innerSql = new StringBuilder(string.Format("SELECT {0} FROM {1}",
                columns,
                GetTableName(classMap)));

            if (classMap.HaveLeftJoinProperties)
            {
                innerSql.Append(BuildLeftJoinAndOn(classMap));
            }

            if (predicate != null)
            {
                innerSql.Append(" WHERE ")
                    .Append(predicate.GetSql(this, parameters));
            }

            string orderBy = sort.Select(s => GetColumnName(classMap, s.PropertyName, false) + (s.Ascending ? " ASC" : " DESC")).AppendStrings();
            innerSql.Append(" ORDER BY " + orderBy);

            string sql = Configuration.Dialect.GetSetSql(innerSql.ToString(), firstResult, maxResults, parameters);
            return sql;
        }


        public virtual string Count(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters");
            }

            StringBuilder sql = new StringBuilder(string.Format("SELECT COUNT(*) AS {0}Total{1} FROM {2}",
                                Configuration.Dialect.OpenQuote,
                                Configuration.Dialect.CloseQuote,
                                GetTableName(classMap)));

            if (classMap.HaveLeftJoinProperties)
            {
                sql.Append(BuildLeftJoinAndOn(classMap));
            }

            if (predicate != null)
            {
                sql.Append(" WHERE ")
                    .Append(predicate.GetSql(this, parameters));
            }

            return sql.ToString();
        }

        public virtual string Insert(IClassMapper classMap)
        {
            var columns = classMap.Properties.Where(p => !(p.Ignored || p.IsLeftJoin || p.IsReadOnly || p.KeyType == KeyType.Identity));
            if (!columns.Any())
            {
                throw new ArgumentException("No columns were mapped.");
            }

            var columnNames = columns.Select(p => GetColumnName(classMap, p, false));
            var parameters = columns.Select(p => Configuration.Dialect.ParameterPrefix + p.Name);

            string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                                       GetTableName(classMap),
                                       columnNames.AppendStrings(),
                                       parameters.AppendStrings());

            return sql;
        }

        public virtual string Update(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("Predicate");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters");
            }

            var columns = classMap.Properties.Where(p => !(p.Ignored || p.IsLeftJoin || p.IsReadOnly || p.KeyType == KeyType.Identity));
            if (!columns.Any())
            {
                throw new ArgumentException("No columns were mapped.");
            }

            var setSql =
                columns.Select(
                    p =>
                    string.Format(
                        "{0} = {1}{2}", GetColumnName(classMap, p, false), Configuration.Dialect.ParameterPrefix, p.Name));

            return string.Format("UPDATE {0} SET {1} WHERE {2}",
                GetTableName(classMap),
                setSql.AppendStrings(),
                predicate.GetSql(this, parameters));
        }

        public virtual string Delete(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("Predicate");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters");
            }

            StringBuilder sql = new StringBuilder(string.Format("DELETE FROM {0}", GetTableName(classMap)));
            sql.Append(" WHERE ").Append(predicate.GetSql(this, parameters));
            return sql.ToString();
        }

        #region Build sql
        public virtual string IdentitySql(IClassMapper classMap)
        {
            return Configuration.Dialect.GetIdentitySql(GetTableName(classMap));
        }

        public virtual string GetTableName(IClassMapper map)
        {
            return Configuration.Dialect.GetTableName(map.SchemaName, map.TableName, null);
        }

        public virtual string GetColumnName(IClassMapper map, IPropertyMap property, bool includeAlias)
        {
            string alias = null;
            if (property.ColumnName != property.Name && includeAlias)
            {
                alias = property.Name;
            }

            return Configuration.Dialect.GetColumnName(GetTableName(map), property.ColumnName, alias);
        }

        public virtual string GetColumnName(IClassMapper map, string propertyName, bool includeAlias)
        {
            IPropertyMap propertyMap = map.Properties.SingleOrDefault(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
            if (propertyMap == null)
            {
                throw new ArgumentException(string.Format("Could not find '{0}' in Mapping.", propertyName));
            }

            return GetColumnName(map, propertyMap, includeAlias);
        }

        public virtual bool SupportsMultipleStatements()
        {
            return Configuration.Dialect.SupportsMultipleStatements;
        }

        public virtual string BuildSelectColumns(IClassMapper classMap)
        {
            var query = classMap.Properties
                .Where(p => !p.Ignored);

            if (!string.IsNullOrEmpty(classMap.MultiQueryIgnoreColumns))
            {
                query = query.Where(p => !classMap.MultiQueryIgnoreColumns.Contains(p.ColumnName));
            }
            var columns = query.Select(p => GetColumnName(classMap, p, true));

            return columns.AppendStrings();
        }

        #region Build left join
        public virtual string BuildSelectColumnsByLeftJoin(IClassMapper classMap)
        {
            //T1
            string columnsSql = BuildSelectColumns(classMap);

            if (classMap.HaveLeftJoinProperties)
            {
                //left join T2...
                foreach (var p in classMap.Properties.Where(p => p.IsLeftJoin))
                {
                    columnsSql += "," + BuildSelectColumns(p.LeftJoinMapper);
                }
            }

            return columnsSql;
        }

        public virtual string BuildLeftJoinAndOn(IClassMapper classMap)
        {
            StringBuilder sql = new StringBuilder();
            if (classMap.HaveLeftJoinProperties)
            {
                foreach (var p in classMap.Properties.Where(p => p.IsLeftJoin))
                {
                    sql.AppendFormat(" LEFT JOIN {0} on {1}.[{2}]={0}.[{3}] ",
                        GetTableName(p.LeftJoinMapper),
                        GetTableName(classMap),
                        p.LeftJoinForeignColumnName,
                        p.LeftJoinPrimaryColumnName
                        );
                }
            }

            return sql.ToString().TrimEnd();
        }
        #endregion
        #endregion
    }
}