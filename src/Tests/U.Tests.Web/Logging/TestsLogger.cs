using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using U.Web.Utils;
using U.Logging;

namespace U.Tests.Web.Logging
{
    public class TestsLogger : U.Logging.ILogger
    {
        public bool IsEnabled(U.Logging.LogLevel level)
        {
            return true;
        }

        public void Log(U.Logging.LogLevel level, Exception exception, string format, params object[] args)
        {
            try
            {
                FileStream fs = new FileStream(RequestHelper.MapPath("~/Logging/log.txt"), FileMode.Create);

                char[] charData = format.ToCharArray();

                byte[] byData = new byte[charData.Length];

                Encoder encoder = Encoding.UTF8.GetEncoder();

                encoder.GetBytes(charData, 0, charData.Length, byData, 0, true);

                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(byData, 0, byData.Length);
                fs.Close();
            }
            catch (Exception ex) { 
            
            }

        }
    }
}