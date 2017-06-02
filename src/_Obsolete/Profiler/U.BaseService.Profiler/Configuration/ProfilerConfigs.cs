using U.Xml.Configuration;

namespace U.BaseService.Profiler.Configuration
{
    /// <summary>
    /// Redis配置文件操作类
    /// </summary>
    public class ProfilerConfigs
    {
        private static System.Timers.Timer baseConfigTimer = new System.Timers.Timer(60000);

        private static ProfilerConfigInfo o_baseConfigInfo;

        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static ProfilerConfigs()
        {
            o_baseConfigInfo = ProfilerConfigFileManager.LoadConfig();
            baseConfigTimer.AutoReset = true;
            baseConfigTimer.Enabled = true;
            baseConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            baseConfigTimer.Start();
        }

        static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }


        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            o_baseConfigInfo = ProfilerConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 重设真正配置类实例
        /// </summary>
        public static void ResetRealConfig()
        {
            o_baseConfigInfo = ProfilerConfigFileManager.LoadRealConfig();
        }

        /// <summary>
        /// 获取本配置类实例
        /// </summary>
        /// <returns></returns>
        public static ProfilerConfigInfo GetConfig()
        {
            return o_baseConfigInfo;
        }

        /// <summary>
        /// 保存配置实例
        /// </summary>
        /// <param name="baseConfigInfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(ProfilerConfigInfo baseConfigInfo)
        {
            ProfilerConfigFileManager bcfm = new ProfilerConfigFileManager();
            ProfilerConfigFileManager.ConfigInfo = baseConfigInfo;
            return bcfm.SaveConfig();
        }
    }
}
