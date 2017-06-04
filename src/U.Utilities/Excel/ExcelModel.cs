//********************************************************************************************************
//*  Creator by： heyben 2017/6/4 10:10:10
//*  Lastest update by： 
//*  Lastest update remark：
//********************************************************************************************************
using System.Collections.Generic;

namespace U.Utilities.Excel
{
    /// <summary>
    /// Excel 表头
    /// </summary>
    public class ExcelThead
    {
        List<string> _items;
        public ExcelThead()
        {
            _items = new List<string>();
        }

        public List<string> Items { get { return _items; } }
    }

    /// <summary>
    /// Excel 数据行（一般数量跟列对应起来）
    /// </summary>
    public class ExcelRow {
        List<List<string>> _rows;
        public ExcelRow() {
            _rows = new List<List<string>>();
        }

        public List<List<string>> Rows { get { return _rows; } }
    }
}
