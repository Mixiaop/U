using System;
using System.IO;
using U.Utilities.Xml;

namespace U.Utilities.Configuration
{
    /// <summary>
    /// 文件配置管理基类
    /// </summary>
    public class DefaultConfigFileManager
    {
        /// <summary>
        /// 文件所在路径
        /// </summary>
        private static string _configFilePath;

        /// <summary>
        /// 临时配置对象
        /// </summary>
        private static IConfigInfo o_configInfo = null;

        /// <summary>
        /// 锁对象
        /// </summary>
        private static object o_lockHelper = new object();

        /// <summary>
        /// 文件所在路径
        /// </summary>
        public static string ConfigFilePath
        {
            get { return _configFilePath; }
            set { _configFilePath = value; }
        }

        /// <summary>
        /// 临时配置对象
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return o_configInfo; }
            set { o_configInfo = value; }
        }

        /// <summary>
        /// 加载(反序列化)指定对象类型的配置对象
        /// </summary>
        /// <param name="fileOldChange">文件加载时间</param>
        /// <param name="configFilePath">配置文件所在路径(包括文件名)</param>
        /// <param name="configInfo">相应的变量 注:该参数主要用于设置o_configinfo变量和获取类型.GetType()</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime fileOldChange, string configFilePath, IConfigInfo configInfo)
        {
            return LoadConfig(ref fileOldChange, configFilePath, configInfo, true);
        }

        /// <summary>
        /// 加载(反序列化)指定对象类型的配置对象
        /// </summary>
        /// <param name="fileOldChange">文件加载时间</param>
        /// <param name="configFilePath">配置文件所在路径(包括文件名)</param>
        /// <param name="configInfo">相应的变量 注:该参数主要用于设置o_configinfo变量和获取类型.GetType()</param>
        /// <param name="checkTime">是否检查并更新传递进来的"文件加载时间"变量</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime fileOldChange, string configFilePath, IConfigInfo configInfo, bool checkTime)
        {
            lock (o_lockHelper)
            {
                _configFilePath = configFilePath;
                o_configInfo = configInfo;
                if (checkTime)
                {
                    DateTime fileNewChange = System.IO.File.GetLastWriteTime(configFilePath); //读取文件最后写入时间

                    //当程序运行中config文件发生变化时则对config重新赋值
                    if (fileOldChange != fileNewChange)
                    {
                        fileOldChange = fileNewChange;
                        o_configInfo = DeserializeInfo(configFilePath, configInfo.GetType());
                    }
                }
                else
                {
                    o_configInfo = DeserializeInfo(configFilePath, configInfo.GetType());
                }
                return o_configInfo;
            }
        }

        /// <summary>
        /// 反序列化指定的类
        /// </summary>
        /// <param name="configFilePath">config文件的路径</param>
        /// <param name="configType">相应的类型</param>
        /// <returns></returns>
        protected static IConfigInfo DeserializeInfo(string configFilePath, Type configType)
        {
            return (IConfigInfo)SerializationHelper.Load(configType, configFilePath);
        }

        /// <summary>
        /// 保存配置实例(虚方法需继承)
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveConfig()
        {
            return true;
        }

        /// <summary>
        /// 保存(序列化)指定路径下的配置文件
        /// </summary>
        /// <param name="configFilePath">指定的配置文件所在的路径(包括文件名)</param>
        /// <param name="configinfo">被保存(序列化)的对象</param>
        /// <returns></returns>
        public bool SaveConfig(string configFilePath, IConfigInfo configinfo)
        {
            return SerializationHelper.Save(configinfo, configFilePath);
        }
    }
}
