using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;

namespace U.Utilities.SqlServer
{
    /// <summary>
    /// sql操作帮助类
    /// </summary>
    public class SqlHelper
    {
        public static string ConnectionString {
            get {
                return DatabaseConfigs.GetConfig().DbConnectinString;
            }
        }

        /// <summary>
        /// SqlBulkCopy批量导入数据
        /// </summary>
        /// <param name="dt">内存表</param>
        /// <param name="table">table要与数据库中的表名称对应</param>
        /// <returns></returns>
        public static string SqlBulkCopySql(DataTable dt, string table, Hashtable hashTable)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default,
                               transaction))
                    {
                        foreach (DictionaryEntry de in hashTable)
                        {
                            bulkCopy.ColumnMappings.Add(de.Key.ToString(), de.Value.ToString());
                        }
                        //每次插入的行数
                        bulkCopy.BatchSize = 5000;
                        //表名称
                        bulkCopy.DestinationTableName = table;
                        try
                        {
                            bulkCopy.WriteToServer(dt);
                            transaction.Commit();
                            return dt.Rows.Count.ToString();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return ex.Message;
                        }
                        finally
                        {
                            conn.Close();
                            bulkCopy.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条语句，获取一个DataReader，调用之后一定要关闭DataReader
        /// </summary>
        /// <param name="sqlStr">要执行的SQL语句</param>
        /// <returns>返回一个DataReader对象</returns>
        public static SqlDataReader GetReader(string sqlStr)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 执行一个带参数的语句，获取一个DataReader，调用之后一定要关闭DataReader
        /// </summary>
        /// <param name="sqlStr">要执行的带参数的SQL语句</param>
        /// <param name="ps">参数列表，数组型参数</param>
        /// <returns>返回一个DataReader对象</returns>
        public static SqlDataReader GetReader(string sqlStr, params SqlParameter[] ps)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddRange(ps);
                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 返回一列数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                try
                {
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
                finally
                {
                    cmd.Dispose();
                    conn.Dispose();
                }
            }

        }

        /// <summary>
        /// 执行一条非查询的SQL语句（Insert Update Delete），返回受影响的行数
        /// </summary>
        /// <param name="sqlStr">要执行的SQL语句</param>
        /// <returns>返回受影响的函数</returns>
        public static string ExecuteSql(string sqlStr)
        {
            //   SqlTransaction trans = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                //    trans = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {
                    try
                    {
                        // cmd.Transaction = trans;
                        int count = cmd.ExecuteNonQuery();
                        // trans.Commit();
                        return count.ToString();
                    }
                    catch (Exception ex)
                    {
                        //trans.Rollback();
                        //trans.Dispose();
                        cmd.Dispose();
                        conn.Dispose();
                        return ex.Message;
                    }
                    finally
                    {
                        cmd.Dispose();
                        conn.Dispose();
                        conn.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条非查询的带参数的SQL语句（Insert Update Delete），返回受影响的行数
        /// </summary>
        /// <param name="sqlStr">要执行的SQL语句</param>
        /// <param name="ps">参数列表，数组型参数</param>
        /// <returns>返回受影响的函数</returns>
        public static int ExecuteSql(string sqlStr, params SqlParameter[] ps)
        {
            SqlTransaction trans = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                trans = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {
                    try
                    {
                        cmd.Transaction = trans;
                        cmd.Parameters.AddRange(ps);
                        int count = cmd.ExecuteNonQuery();
                        trans.Commit();
                        return count;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        trans.Dispose();
                        cmd.Dispose();
                        return 0;
                    }
                    finally
                    {
                        cmd.Dispose();
                        conn.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条SQL语句，返回一个被填充的DataTable
        /// </summary>
        /// <param name="sqlStr">要执行的SQL语句</param>
        /// <returns>一个DataTable对象</returns>
        public static DataTable GetDataTable(string sqlStr)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter(sqlStr, ConnectionString))
            {
                try
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
                finally
                {
                    sda.Dispose();
                }
            }
        }

        /// <summary>
        /// 执行一条带参数的SQL语句，返回一个被填充的DataTable
        /// </summary>
        /// <param name="sqlStr">要执行的SQL语句</param>
        /// <param name="ps">参数列表，数组型参数</param>
        /// <returns>一个DataTable对象</returns>
        public static DataTable GetDataTable(string sqlStr, params SqlParameter[] ps)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {
                    cmd.Parameters.AddRange(ps);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                        finally
                        {
                            sda.Dispose();
                            cmd.Dispose();
                            conn.Dispose();
                        }
                    }
                }
            }
        }
    }
}
