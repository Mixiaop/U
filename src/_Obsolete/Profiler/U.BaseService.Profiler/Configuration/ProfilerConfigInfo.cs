using System;
using U.Xml.Configuration;

namespace U.BaseService.Profiler.Configuration
{
   
    [Serializable]
    public class ProfilerConfigInfo : IConfigInfo
    {
        /// <summary>
        /// 是否开启
        /// </summary>
        public bool Apply { get; set; }
    }
}
