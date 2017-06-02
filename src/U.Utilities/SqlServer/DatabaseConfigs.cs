
namespace U.Utilities.SqlServer
{
    /// <summary>
    /// 数据连接配置操作类
    /// </summary>
    public class DatabaseConfigs
    {
        private static System.Timers.Timer baseConfigTimer = new System.Timers.Timer(60000);

        private static DatabaseConfigInfo o_baseConfigInfo;

        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static DatabaseConfigs()
        {
            o_baseConfigInfo = DatabaseConfigFileManager.LoadConfig();
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
        /// 获取数据库连接
        /// </summary>
        public static string DbConnectinString
        {
            get
            {
                return o_baseConfigInfo.DbConnectinString;
            }
        }

        /// <summary>
        /// 获取数据库类型
        /// </summary>
        public static string DbType
        {
            get
            {
                return o_baseConfigInfo.DbType;
            }
        }

        /// <summary>
        /// 获取数据库提供者名称
        /// </summary>
        public static string DbProviderName
        {
            get
            {
                switch (o_baseConfigInfo.DbType)
                {
                    case "SqlServer":
                        return "System.Data.SqlClient";

                }
                return "System.Data.SqlClient";
            }
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            o_baseConfigInfo = DatabaseConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 重设真正配置类实例
        /// </summary>
        public static void ResetRealConfig()
        {
            o_baseConfigInfo = DatabaseConfigFileManager.LoadRealConfig();
        }

        /// <summary>
        /// 获取本配置类实例
        /// </summary>
        /// <returns></returns>
        public static DatabaseConfigInfo GetConfig()
        {
            return o_baseConfigInfo;
        }

        /// <summary>
        /// 保存配置实例
        /// </summary>
        /// <param name="baseconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(DatabaseConfigInfo baseConfigInfo)
        {
            DatabaseConfigFileManager bcfm = new DatabaseConfigFileManager();
            DatabaseConfigFileManager.ConfigInfo = baseConfigInfo;
            return bcfm.SaveConfig();
        }
    }
}
