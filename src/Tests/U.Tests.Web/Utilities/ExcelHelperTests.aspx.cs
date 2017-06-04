using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using U.Utilities.Web;
using U.Utilities.Excel;


namespace U.Tests.Web.Utilities
{
    public partial class ExcelHelperTests : System.Web.UI.Page
    {
        string datasourceFormat = "Provider=Microsoft.Ace.OleDb.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
        protected void Page_Load(object sender, EventArgs e)
        {
            btnToExcel.Click += btnToExcel_Click;
            //var path = WebHelper.MapPath("/Utilities/2014 DoubanMovieLinks.xlsx");

            //var dt = ExcelHelper.ExcelToDataTable(path, "Sheet1", datasourceFormat);
            //foreach (DataRow row in dt.Rows)
            //{
            //    var name = row[0];
            //    var link = row[1];
            //    var a = 1;
            //}
        }

        void btnToExcel_Click(object sender, EventArgs e)
        {
            var thead = new ExcelThead();
            var row = new ExcelRow();

            thead.Items.Add("交易单");
            thead.Items.Add("应用对象");
            thead.Items.Add("价格");
            thead.Items.Add("手机号码");
            thead.Items.Add("创建时间");

            var row1 = new List<string>();
            var row2 = new List<string>();
            var row3 = new List<string>();
            var row4 = new List<string>();

            row1.Add("201515115");
            row1.Add("填志愿");
            row1.Add("1.0");
            row1.Add("15800448791");
            row1.Add(DateTime.Now.ToString());

            row2.Add("201515115");
            row2.Add("填志愿");
            row2.Add("1.0");
            row2.Add("15800448791");
            row2.Add(DateTime.Now.ToString());

            row3.Add("201515115");
            row3.Add("填志愿");
            row3.Add("1.0");
            row3.Add("15800448791");
            row3.Add(DateTime.Now.ToString());

            row4.Add("201515115");
            row4.Add("填志愿");
            row4.Add("1.0");
            row4.Add("15800448791");
            row4.Add(DateTime.Now.ToString());
            row.Rows.Add(row1);
            row.Rows.Add(row2);
            row.Rows.Add(row3);
            row.Rows.Add(row4);
            ExcelHelper.ToExcelAndDownload("支付交易单", thead, row);
            //row.Rows.Add(new List<sting>()
        }
    }
}