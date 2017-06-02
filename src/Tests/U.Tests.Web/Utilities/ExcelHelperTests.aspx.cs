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
            var path = WebHelper.MapPath("/Utilities/2014 DoubanMovieLinks.xlsx");

            var dt = ExcelHelper.ExcelToDataTable(path, "Sheet1", datasourceFormat);
            foreach (DataRow row in dt.Rows)
            {
                var name = row[0];
                var link = row[1];
                var a = 1;
            }
        }
    }
}