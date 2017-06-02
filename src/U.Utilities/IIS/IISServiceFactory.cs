using System;
using Microsoft.Win32;

namespace U.Utilities.IIS
{
    public class IISServiceFactory
    {
        public static IIISUtilService GetUtilService()
        {
            IIISUtilService instance = null;

            int majorVersion = GetIISMajorVersion();
            switch (majorVersion)
            {
                case 6:
                    //instance = new IIS6UtilService();
                    break;
                case 7:
                case 8:
                    instance = new IIS7UtilService();
                    break;
                default:
                    instance = new IIS7UtilService();
                    break;
            }

            return instance;
        }

        public static int GetIISMajorVersion()
        {
            int majorVersion = 0;

            try
            {
                var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\INetStp");
                if (key != null)
                    majorVersion = Convert.ToInt32(key.GetValue("MajorVersion"));
            }
            catch { }

            return majorVersion;
        }
    }
}
