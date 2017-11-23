using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using U;
using U.Segmentation;
using U.Utilities.Web;
using U.Segmentation.Jieba.Analyser;

namespace U.Tests.Web.USegmentation
{
    public partial class Tests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ISegmenter segmenter = UPrimeEngine.Instance.Resolve<ISegmenter>();
            segmenter.LoadUserDict("custom_dict.txt");
            var ss = "新乡医学院好还是不好";
            var list = segmenter.Cut(ss, false);
            Response.Write(ss);
            Response.Write("<br />");
            foreach (string s in list)
            {
                if (s.Length > 1 && s.IsNotNullOrEmpty())
                {
                    Response.Write(s + " / ");
                }
            }

            //var tfidf = new TfidfExtractor();
            //var text = GetFileContents(WebHelper.MapPath("/USegmentation/Resources/article_sports.txt"));
            //var result = tfidf.ExtractTags(text, 30);
            //Response.Write("TfidfExtractor：<br />");
            //foreach (var tag in result)
            //{
            //    Response.Write(tag + "<br />");
            //}

            //Response.Write("TextRankExtractor：<br />");

            //var s = GetFileContents(WebHelper.MapPath("/USegmentation/Resources/article.txt"));
            //var extractor = new TextRankExtractor();
            //var result2 = extractor.ExtractTags(s);
            //foreach (var tag in result2)
            //{
            //    Response.Write(tag + "<br />");
            //}
        }

        private string GetFileContents(string fileName)
        {
            return File.ReadAllText(fileName);
        }
    }
}