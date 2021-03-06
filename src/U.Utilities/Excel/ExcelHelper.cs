﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Text;

namespace U.Utilities.Excel
{
    /// <summary>
    /// Excel操作类
    /// </summary>
    public class ExcelHelper
    {
        #region DataToExcel
        ///// <summary>
        ///// 将数据导出至Excel文件
        ///// </summary>
        ///// <param name="table">DataTable对象</param>
        ///// <param name="columns">要导出的数据列集合</param>
        ///// <param name="excelFilePath">Excel文件路径</param>
        //public static bool OutputToExcel(DataTable table, List<string> columns, string excelFilePath)
        //{
        //    if (File.Exists(excelFilePath))
        //    {
        //        throw new Exception("该文件已经存在！");
        //    }

        //    //如果数据列数大于表的列数，取数据表的所有列
        //    if (columns.Count > table.Columns.Count)
        //    {
        //        for (int s = table.Columns.Count + 1; s <= columns.Count; s++)
        //        {
        //            columns.RemoveAt(s);   //移除数据表列数后的所有列
        //        }
        //    }

        //    //遍历所有的数据列，如果有数据列的数据类型不是 DataColumn，则将它移除
        //    DataColumn column = new DataColumn();
        //    for (int j = 0; j < columns.Count; j++)
        //    {
        //        try
        //        {
        //            column = (DataColumn)columns[j];
        //        }
        //        catch (Exception)
        //        {
        //            columns.RemoveAt(j);
        //        }
        //    }
        //    if ((table.TableName.Trim().Length == 0) || (table.TableName.ToLower() == "table"))
        //    {
        //        table.TableName = "Sheet1";
        //    }

        //    //数据表的列数
        //    int ColCount = Columns.Count;

        //    //创建参数
        //    OleDbParameter[] para = new OleDbParameter[ColCount];

        //    //创建表结构的SQL语句
        //    string TableStructStr = @"Create Table " + Table.TableName + "(";

        //    //连接字符串
        //    string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;";
        //    OleDbConnection objConn = new OleDbConnection(connString);

        //    //创建表结构
        //    OleDbCommand objCmd = new OleDbCommand();

        //    //数据类型集合
        //    ArrayList DataTypeList = new ArrayList();
        //    DataTypeList.Add("System.Decimal");
        //    DataTypeList.Add("System.Double");
        //    DataTypeList.Add("System.Int16");
        //    DataTypeList.Add("System.Int32");
        //    DataTypeList.Add("System.Int64");
        //    DataTypeList.Add("System.Single");

        //    DataColumn col = new DataColumn();

        //    //遍历数据表的所有列，用于创建表结构
        //    for (int k = 0; k < ColCount; k++)
        //    {
        //        col = (DataColumn)Columns[k];

        //        //列的数据类型是数字型
        //        if (DataTypeList.IndexOf(col.DataType.ToString().Trim()) >= 0)
        //        {
        //            para[k] = new OleDbParameter("@" + col.Caption.Trim(), OleDbType.Double);
        //            objCmd.Parameters.Add(para[k]);

        //            //如果是最后一列
        //            if (k + 1 == ColCount)
        //            {
        //                TableStructStr += col.Caption.Trim() + " Double)";
        //            }
        //            else
        //            {
        //                TableStructStr += col.Caption.Trim() + " Double,";
        //            }
        //        }
        //        else
        //        {
        //            para[k] = new OleDbParameter("@" + col.Caption.Trim(), OleDbType.VarChar);
        //            objCmd.Parameters.Add(para[k]);

        //            //如果是最后一列
        //            if (k + 1 == ColCount)
        //            {
        //                TableStructStr += col.Caption.Trim() + " VarChar)";
        //            }
        //            else
        //            {
        //                TableStructStr += col.Caption.Trim() + " VarChar,";
        //            }
        //        }
        //    }

        //    //创建Excel文件及文件结构
        //    try
        //    {
        //        objCmd.Connection = objConn;
        //        objCmd.CommandText = TableStructStr;

        //        if (objConn.State == ConnectionState.Closed)
        //        {
        //            objConn.Open();
        //        }
        //        objCmd.ExecuteNonQuery();
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }

        //    //插入记录的SQL语句
        //    string InsertSql_1 = "Insert into " + Table.TableName + " (";
        //    string InsertSql_2 = " Values (";
        //    string InsertSql = "";

        //    //遍历所有列，用于插入记录，在此创建插入记录的SQL语句
        //    for (int colID = 0; colID < ColCount; colID++)
        //    {
        //        if (colID + 1 == ColCount)  //最后一列
        //        {
        //            InsertSql_1 += Columns[colID].ToString().Trim() + ")";
        //            InsertSql_2 += "@" + Columns[colID].ToString().Trim() + ")";
        //        }
        //        else
        //        {
        //            InsertSql_1 += Columns[colID].ToString().Trim() + ",";
        //            InsertSql_2 += "@" + Columns[colID].ToString().Trim() + ",";
        //        }
        //    }

        //    InsertSql = InsertSql_1 + InsertSql_2;

        //    //遍历数据表的所有数据行
        //    DataColumn DataCol = new DataColumn();
        //    for (int rowID = 0; rowID < Table.Rows.Count; rowID++)
        //    {
        //        for (int colID = 0; colID < ColCount; colID++)
        //        {
        //            //因为列不连续，所以在取得单元格时不能用行列编号，列需得用列的名称
        //            DataCol = (DataColumn)Columns[colID];
        //            if (para[colID].DbType == DbType.Double && Table.Rows[rowID][DataCol.Caption].ToString().Trim() == "")
        //            {
        //                para[colID].Value = 0;
        //            }
        //            else
        //            {
        //                para[colID].Value = Table.Rows[rowID][DataCol.Caption].ToString().Trim();
        //            }
        //        }
        //        try
        //        {
        //            objCmd.CommandText = InsertSql;
        //            objCmd.ExecuteNonQuery();
        //        }
        //        catch (Exception exp)
        //        {
        //            string str = exp.Message;
        //        }
        //    }
        //    try
        //    {
        //        if (objConn.State == ConnectionState.Open)
        //        {
        //            objConn.Close();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //    return true;
        //}
        #endregion

        #region ToExcel
        /// <summary>
        /// 通过指定的表头与行生成Excel，直接输出下载
        /// </summary>
        /// <param name="fileName">输出文件名</param>
        /// <param name="thead"></param>
        /// <param name="rows"></param>
        public static void ToExcelAndDownload(string fileName, ExcelThead thead, ExcelRow rows)
        {
            #region Thead and rows
            StringBuilder sbRows = new StringBuilder();
            if (thead != null && thead.Items.Count > 0)
            {
                sbRows.Append("<Row>");
                foreach (var item in thead.Items)
                {
                    sbRows.Append(string.Format("<Cell ss:StyleID=\"s21\"><Data ss:Type=\"String\">{0}</Data></Cell>", item));
                }
                sbRows.Append("</Row>");
            }
            if (rows != null && rows.Rows.Count > 0)
            {
                foreach (var data in rows.Rows)
                {
                    sbRows.Append("<Row>");

                    foreach (var rowItem in data)
                    {
                        sbRows.Append(string.Format("<Cell ss:StyleID=\"s21\"><Data ss:Type=\"String\">{0}</Data></Cell>", rowItem));
                    }

                    sbRows.Append("</Row>");
                }
            }
            #endregion

            #region ToExcel
            int pageRecord = 65536;//显示表头时为65535,否则为65536
            if (thead != null)
            {
                pageRecord = 65535;
            }
            
            //worksheet
            //int sheetCount = recordCount / pageRecord + 1;
            //excel表头
            string head = "<?xml version=\"1.0\"?>"
                + "<?mso-application progid=\"Excel.Sheet\"?>"
                + "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\""
                + " xmlns:o=\"urn:schemas-microsoft-com:office:office\""
                + " xmlns:x=\"urn:schemas-microsoft-com:office:excel\""
                + " xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\""
                + " xmlns:html=\"http://www.w3.org/TR/REC-html40\">"
                + " <Styles>"
                + " <Style ss:ID=\"s11\">"
                + " <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>"
                + " <Font ss:FontName=\"微软雅黑\" ss:Size=\"12\"/>"
                + " <Interior/>"
                + " <NumberFormat/>"
                + " <Protection/>"
                + " </Style>"
                + " <Style ss:ID=\"s12\">"
                + " <Borders>"
                + " <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + " <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + " <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + " <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + " </Borders>"
                + " <Font ss:FontName=\"微软雅黑\" ss:Size=\"11\" ss:Bold=\"1\"/>"
                + " <Interior ss:Color=\"#CCFFCC\" ss:Pattern=\"Solid\"/>"
                + " </Style>"
                + " <Style ss:ID=\"s13\">"
                + " <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Center\"/>"
                + " <Font ss:FontName=\"微软雅黑\" ss:Size=\"11\"/>"
                + " <Interior/>"
                + " <NumberFormat/>"
                + " <Protection/>"
                + " </Style>"
                + " <Style ss:ID=\"s14\">"
                + " <Borders>"
                + " <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + " <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + " <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + " <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + " </Borders>"
                + " <Font x:Family=\"Swiss\" ss:Size=\"11\"/>"
                + " </Style>"
                + " <Style ss:ID=\"s21\">"
                + " <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>"
                + " <Borders/>"
                + " <Font ss:FontName=\"微软雅黑\" ss:Size=\"12\"/>"
                + " <Interior/>"
                + " <NumberFormat/>"
                + " <Protection/>"
                + " </Style>"
                + " <Style ss:ID=\"s25\">"
                + " <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>"
                + " <Borders/>"
                + " <Font ss:FontName=\"微软雅黑\" ss:Size=\"12\"/>"
                + " <Interior ss:Color=\"#99CCFF\" ss:Pattern=\"Solid\"/>"
                + " <NumberFormat/>"
                + " <Protection/>"
                + " </Style>"
                + " </Styles>";

            //excel的xml格式
            StringBuilder body = new StringBuilder(10000);
            body.AppendFormat("<Worksheet ss:Name=\"{0}\">", "Sheet1");
            body.Append("<Table>");
            body.Append(sbRows.ToString());
            body.Append("</Table>");
            body.Append("</Worksheet>");
            body.Append("</Workbook>");

            //输出到客户端
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(fileName) + ".xls");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(head + body.ToString());
            HttpContext.Current.Response.End();
            #endregion
        }
        #endregion

        #region ExcelToData
        /// <summary>
        /// 获取Excel文件数据表列表
        /// <param name="excelFilePath"></param>
        /// <param name="dataSourceFormat"></param>
        /// </summary>
        public static List<string> GetExcelSheets(string excelFilePath, string connectionFormat = "Provider=Microsoft.Ace.OleDb.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'")
        {
            DataTable dt = new DataTable();
            List<string> sheets = new List<string>();
            if (File.Exists(excelFilePath))
            {
                string connectionStr = string.Format(connectionFormat, excelFilePath);
                using (OleDbConnection conn = new OleDbConnection(connectionStr))
                {
                    try
                    {
                        conn.Open();
                        dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    }
                    catch (Exception exp)
                    {
                        throw exp;
                    }

                    //获取数据表个数
                    int tablecount = dt.Rows.Count;
                    for (int i = 0; i < tablecount; i++)
                    {
                        string tablename = dt.Rows[i][2].ToString().Trim().TrimEnd('$');
                        if (sheets.IndexOf(tablename) < 0)
                        {
                            sheets.Add(tablename);
                        }
                    }
                }
            }
            return sheets;
        }

        /// <summary>
        /// 将Excel文件导出至DataTable(第一行作为表头)
        /// </summary>
        /// <param name="excelFilePath">Excel文件路径</param>
        /// <param name="sheetName">数据表名，如果数据表名错误，默认为第一个数据表名</param>
        /// <param name="connectionFormat"></param>
        public static DataTable ExcelToDataTable(string excelFilePath, string sheetName = "Sheet1", string connectionFormat = "Provider=Microsoft.Ace.OleDb.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'")
        {
            if (!File.Exists(excelFilePath))
            {
                throw new Exception("Excel文件不存在！");
            }

            //如果数据表名不存在，则数据表名为Excel文件的第一个数据表
            List<string> sheets = new List<string>();
            sheets = GetExcelSheets(excelFilePath, connectionFormat);

            if (!sheets.Contains(sheetName))
            {
                throw new Exception("表名【" + sheetName + "】不存在");
            }

            DataTable table = new DataTable();
            OleDbConnection dbcon = new OleDbConnection(string.Format(connectionFormat, excelFilePath));
            OleDbCommand cmd = new OleDbCommand("select * from [" + sheetName + "$]", dbcon);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            try
            {
                if (dbcon.State == ConnectionState.Closed)
                {
                    dbcon.Open();
                }
                adapter.Fill(table);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (dbcon.State == ConnectionState.Open)
                {
                    dbcon.Close();
                }
            }
            return table;
        }

        /// <summary>
        /// 获取Excel文件指定数据表的数据列表
        /// </summary>
        /// <param name="excelFilePath">Excel文件名</param>
        /// <param name="sheetName">数据表名</param>
        /// <param name="connectionFormat"></param>
        public static List<string> GetExcelColumns(string excelFilePath, string sheetName = "Sheet1", string connectionFormat = "Provider=Microsoft.Ace.OleDb.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'")
        {
            DataTable dt = new DataTable();
            List<string> columns = new List<string>();
            if (File.Exists(excelFilePath))
            {
                using (OleDbConnection conn = new OleDbConnection(string.Format(connectionFormat, excelFilePath)))
                {
                    conn.Open();
                    dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, sheetName, null });

                    //获取列个数
                    int colcount = dt.Rows.Count;
                    for (int i = 0; i < colcount; i++)
                    {
                        string colname = dt.Rows[i]["Column_Name"].ToString().Trim();
                        columns.Add(colname);
                    }
                }
            }
            return columns;
        }
        #endregion
    }
}
