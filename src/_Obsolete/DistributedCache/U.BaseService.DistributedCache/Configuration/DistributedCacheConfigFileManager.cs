﻿using System;
using System.IO;
using System.Web;
using U.Web.Utils;
using U.Xml.Configuration;

namespace U.BaseService.DistributedCache.Configuration
{
    class DistributedCacheConfigFileManager : DefaultConfigFileManager
    {
        /// <summary>
        /// 配置描述类
        /// </summary>
        private static DistributedCacheConfigInfo o_configInfo;

        /// <summary>
        /// 锁对象
        /// </summary>
        private static object o_lockHelper = new object();

        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime fileOldChange;


        /// <summary>
        /// 静态构造器：初始化文件修改时间和对象实例
        /// </summary>
        static DistributedCacheConfigFileManager()
        {
            fileOldChange = System.IO.File.GetLastWriteTime(ConfigFilePath);
            try
            {
                o_configInfo = (DistributedCacheConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(DistributedCacheConfigInfo));
            }
            catch
            { //第一次异常还示做处理
            }
        }

        /// <summary>
        /// 配置文件所在路径
        /// </summary>
        public static string fileName = null;

        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public new static IConfigInfo ConfigInfo
        {
            get { return o_configInfo; }
            set { o_configInfo = (DistributedCacheConfigInfo)value; }
        }
        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public new static string ConfigFilePath
        {
            get
            {
                if (fileName == null)
                {
                    HttpContext context = HttpContext.Current;

                    if (context != null)
                    {
                        fileName = context.Server.MapPath("~/Config/YOUF/DistributedCache.config");
                        if (!File.Exists(fileName))
                        {
                            fileName = context.Server.MapPath("~/Config/YOUF/DistributedCache.config");
                        }
                    }
                    else
                    {
                        fileName = RequestHelper.MapPath("~/Config/YOUF/DistributedCache.config");
                    }

                    if (!File.Exists(fileName))
                    {

                        throw new Exception("发生错误: 虚拟目录或网站根目录下没有正确的~/Config/YOUF/DistributedCache.config文件");
                    }
                }
                return fileName;
            }
        }

        /// <summary>
        /// 加载配置类
        /// </summary>
        /// <returns></returns>
        public static DistributedCacheConfigInfo LoadConfig()
        {
            ConfigInfo = DefaultConfigFileManager.LoadConfig(ref fileOldChange, ConfigFilePath, ConfigInfo);
            return ConfigInfo as DistributedCacheConfigInfo;
        }

        /// <summary>
        /// 加载真正有效的配置类
        /// </summary>
        /// <returns></returns>
        public static DistributedCacheConfigInfo LoadRealConfig()
        {
            ConfigInfo = DefaultConfigFileManager.LoadConfig(ref fileOldChange, ConfigFilePath, ConfigInfo, false);
            return ConfigInfo as DistributedCacheConfigInfo;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns></returns>
        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }

    }
}
