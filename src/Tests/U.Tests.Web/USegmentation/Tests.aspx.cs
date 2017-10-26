using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;
using U.Segmentation;

namespace U.Tests.Web.USegmentation
{
    public partial class Tests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ISegmenter segmenter = UPrimeEngine.Instance.Resolve<ISegmenter>();
            segmenter.LoadUserDict("custom_dict.txt");

            var list = segmenter.Cut("北京大学能不能上？");
            Response.Write(string.Join(" / ", list));
            Response.Write("<br />");
            var list2 = segmenter.CutForSearch("北京大学能不能上？");
            Response.Write(string.Join(" / ", list2));
        }
    }
}