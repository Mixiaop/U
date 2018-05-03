using System.Management;

namespace U.Utilities
{
    public static class CommonHelper
    {
        /// <summary>
        /// 获得进程的用户名
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public static string GetProcessUsername(int pid)
        {
            string username = string.Empty;
            SelectQuery query = new SelectQuery("Select * from Win32_Process WHERE processID=" + pid);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            try
            {
                foreach (ManagementObject disk in searcher.Get())
                {
                    ManagementBaseObject inPar = null;
                    ManagementBaseObject outPar = null;


                    inPar = disk.GetMethodParameters("GetOwner");


                    outPar = disk.InvokeMethod("GetOwner", inPar, null);


                    username = outPar["User"].ToString();
                    break;
                }
            }
            catch
            {
                username = "SYSTEM";
            }


            return username;
        }
    }
}
