using System;
using System.Collections.Generic;
using Microsoft.Web.Administration;

namespace U.Utilities.IIS
{
    /// <summary>
    /// IIS7工具类(IIS8适用)
    /// 如遇到权限问题，可在配置文件里加上<identity impersonate="true" userName="administrator" password="youzy.cn123#@!"/>
    /// </summary>
    public class IIS7UtilService : BaseService, IIISUtilService
    {
        #region 站点
        public int CreateSite(string siteName, int httpPort, int httpsPort, string sslHash, string physicalPath)
        {
            int errorCode = IISErrorCode.Succeed;

            try
            {
                var server = new ServerManager();

                //检查是否已存在，顺便确定端口占用情况
                bool exists = false;
                var usedHttpPorts = new List<int>();		//已使用的http端口
                var usedHttpsPorts = new List<int>();		//已使用的https端口
                foreach (var site in server.Sites)
                {
                    foreach (var binding in site.Bindings)
                    {
                        if (binding.Protocol.EqualsEx("http"))
                            usedHttpPorts.Add(binding.EndPoint.Port);
                        else if (binding.Protocol.EqualsEx("https"))
                            usedHttpsPorts.Add(binding.EndPoint.Port);
                    }

                    if (site.Name.EqualsEx(siteName))
                    {
                        exists = true;
                    }
                }

                //不存在才创建
                if (!exists)
                {
                    //检查端口占用情况
                    if (httpPort > 0 && usedHttpPorts.Contains(httpPort))
                    {
                        //http端口占用
                        errorCode = IISErrorCode.HttpPortUsed;
                    }
                    else if (httpsPort > 0 && usedHttpsPorts.Contains(httpsPort))
                    {
                        //https端口占用
                        errorCode = IISErrorCode.HttpsPortUsed;
                    }
                    else
                    {
                        //开始创建站点
                        Site site = null;
                        if (httpPort > 0)
                        {
                            //建http站点
                            site = server.Sites.Add(siteName, physicalPath, httpPort);
                            if (httpsPort > 0)
                            {
                                //加https绑定
                                site.Bindings.Add(string.Format("*:{0}:", httpsPort), HexString2Bytes(sslHash), "MY");
                            }
                        }
                        else
                        {
                            //直接建https站点
                            site = server.Sites.Add(siteName, string.Format("*:{0}:", httpsPort), physicalPath, HexString2Bytes(sslHash));
                        }
                        site.ServerAutoStart = true;
                        site.LogFile.Enabled = false;			//不记录日志

                        server.CommitChanges();
                    }
                }
                else
                {
                    errorCode = IISErrorCode.SiteExists;
                }
            }
            catch (Exception ex)
            {
                errorCode = IISErrorCode.Unknown;
                Console.WriteLine(ex.Message);
            }

            return errorCode;
        }

        public int RemoveSite(string siteName)
        {
            int errorCode = IISErrorCode.Succeed;

            try
            {
                var server = new ServerManager();

                //找指定站点
                Site targetSite = null;
                foreach (var site in server.Sites)
                {
                    if (site.Name.EqualsEx(siteName))
                    {
                        targetSite = site;
                        break;
                    }
                }

                //存在才移除
                if (targetSite != null)
                {
                    server.Sites.Remove(targetSite);
                    server.CommitChanges();
                }
                else
                {
                    errorCode = IISErrorCode.SiteNotFound;
                }
            }
            catch (Exception ex)
            {
                errorCode = IISErrorCode.Unknown;
                Console.WriteLine(ex.Message);
            }

            return errorCode;
        }
        #endregion

        #region 应用程序池
        public int CreateAppPool(string poolName)
        {
            int errorCode = IISErrorCode.Succeed;

            try
            {
                var server = new ServerManager();

                //先检查是否已存在
                bool exists = false;
                foreach (var pool in server.ApplicationPools)
                {
                    if (pool.Name.EqualsEx(poolName))
                    {
                        exists = true;
                        break;
                    }
                }

                //不存在才创建
                if (!exists)
                {
                    var appPool = server.ApplicationPools.Add(poolName);
                    appPool.ManagedRuntimeVersion = "v4.0";							//指定dotnet4.0，集成模式
                    appPool.ManagedPipelineMode = ManagedPipelineMode.Integrated;
                    appPool.QueueLength = 10000;
                    server.CommitChanges();
                }
                else
                {
                    errorCode = IISErrorCode.AppPoolExists;
                }
            }
            catch (Exception ex)
            {
                errorCode = IISErrorCode.Unknown;
                Console.WriteLine(ex.Message);
            }

            return errorCode;
        }

        public int RemoveAppPool(string poolName)
        {
            int errorCode = IISErrorCode.Succeed;

            try
            {
                var server = new ServerManager();

                //找指定的应用程序池
                ApplicationPool targetPool = null;
                foreach (var pool in server.ApplicationPools)
                {
                    if (pool.Name.EqualsEx(poolName))
                    {
                        targetPool = pool;
                        break;
                    }
                }

                //存在才移除
                if (targetPool != null)
                {
                    server.ApplicationPools.Remove(targetPool);
                    server.CommitChanges();
                }
                else
                {
                    errorCode = IISErrorCode.AppPoolNotFound;
                }
            }
            catch (Exception ex)
            {
                errorCode = IISErrorCode.Unknown;
                Console.WriteLine(ex.Message);
            }

            return errorCode;
        }

        #endregion

        #region 虚拟目录
        public int CreateDir(string siteName, string virtualPath, string physicalPath, bool enableAllMimeTypes)
        {
            int errorCode = IISErrorCode.Succeed;

            //try
            //{
            //    var server = new ServerManager();

            //    //先找站点
            //    Site targetSite = null;
            //    foreach (var site in server.Sites)
            //    {
            //        if (site.Name.EqualsEx(siteName))
            //        {
            //            targetSite = site;
            //            break;
            //        }
            //    }

            //    if (targetSite != null)
            //    {
            //        //找根应用
            //        Application rootApp = null;
            //        foreach (var app in targetSite.Applications)
            //        {
            //            if (app.Path.Equals("/"))
            //            {
            //                rootApp = app;
            //                break;
            //            }
            //        }

            //        //存在根应用才能创建
            //        if (rootApp != null)
            //        {
            //            bool exists = false;
            //            foreach (var dir in rootApp.VirtualDirectories)
            //            {
            //                if (dir.Path.EqualsEx(virtualPath))
            //                {
            //                    exists = true;
            //                    break;
            //                }
            //            }

            //            if (!exists)
            //            {
            //                var dir = rootApp.VirtualDirectories.Add(virtualPath, physicalPath);

            //                //处理Mime Types
            //                if (enableAllMimeTypes)
            //                {
            //                    /*  路径映射存在问题，会向错误的位置写web.config，未解决，改用直接写文件的方法
            //                    var config = server.GetWebConfiguration(targetSite.Name, dir.Path);
            //                    var mimes = config.GetSection("system.webServer/staticContent").GetCollection();

            //                    var mimeTypeSection = mimes.CreateElement("mimeMap");
            //                    mimeTypeSection["fileExtension"] = ".*";
            //                    mimeTypeSection["mimeType"] = "application/*";
            //                    mimes.AddAt(0, mimeTypeSection);
            //                    */
            //                    string path = Path.Combine(physicalPath, "web.config");
            //                    File.WriteAllText(path, IISResource.ResourceManager.GetString("webconfig"), Encoding.UTF8);
            //                }

            //                server.CommitChanges();
            //            }
            //            else
            //            {
            //                errorCode = IISErrorCode.VirtualDirExists;
            //            }
            //        }
            //        else
            //        {
            //            errorCode = IISErrorCode.RootAppNotFound;
            //        }
            //    }
            //    else
            //    {
            //        errorCode = IISErrorCode.SiteNotFound;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    errorCode = IISErrorCode.Unknown;
            //    Console.WriteLine(ex.Message);
            //}

            return errorCode;
        }

        public int RemoveDir(string siteName, string virtualPath)
        {
            int errorCode = IISErrorCode.Succeed;

            //try
            //{
            //    var server = new ServerManager();

            //    //先找站点
            //    Site targetSite = null;
            //    foreach (var site in server.Sites)
            //    {
            //        if (site.Name.EqualsEx(siteName))
            //        {
            //            targetSite = site;
            //            break;
            //        }
            //    }

            //    if (targetSite != null)
            //    {
            //        //找根应用
            //        Application rootApp = null;
            //        foreach (var app in targetSite.Applications)
            //        {
            //            if (app.Path.Equals("/"))
            //            {
            //                rootApp = app;
            //                break;
            //            }
            //        }

            //        //存在根应用才能删除
            //        if (rootApp != null)
            //        {
            //            VirtualDirectory targetDir = null;
            //            foreach (var dir in rootApp.VirtualDirectories)
            //            {
            //                if (dir.Path.EqualsEx(virtualPath))
            //                {
            //                    targetDir = dir;
            //                    break;
            //                }
            //            }

            //            if (targetDir != null)
            //            {
            //                rootApp.VirtualDirectories.Remove(targetDir);
            //                server.CommitChanges();
            //            }
            //            else
            //            {
            //                errorCode = IISErrorCode.VirtualDirNotFound;
            //            }
            //        }
            //        else
            //        {
            //            errorCode = IISErrorCode.RootAppNotFound;
            //        }
            //    }
            //    else
            //    {
            //        errorCode = IISErrorCode.SiteNotFound;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    errorCode = IISErrorCode.Unknown;
            //    Console.WriteLine(ex.Message);
            //}

            return errorCode;
        }

        #endregion

        #region 应用
        public int CreateApp(string siteName, string virtualPath, string physicalPath, string poolName, bool useSsl)
        {
            int errorCode = IISErrorCode.Succeed;

            try
            {
                var server = new ServerManager();

                //先找站点
                Site targetSite = null;
                foreach (var site in server.Sites)
                {
                    if (site.Name.EqualsEx(siteName))
                    {
                        targetSite = site;
                        break;
                    }
                }

                if (targetSite != null)
                {
                    //再找应用
                    bool exists = false;
                    foreach (var app in targetSite.Applications)
                    {
                        if (app.Path.Equals(virtualPath))
                        {
                            exists = true;
                            break;
                        }
                    }

                    //不存在才创建
                    if (!exists)
                    {
                        var app = targetSite.Applications.Add(virtualPath, physicalPath);

                        //处理poolName
                        if (!poolName.IsNullOrEmpty())
                            app.ApplicationPoolName = poolName;

                        //处理ssl
                        if (useSsl)
                        {
                            var root = server.GetApplicationHostConfiguration();
                            var location = root.GetSection("system.webServer/security/access", string.Format("{0}{1}", targetSite.Name, app.Path));
                            location["sslFlags"] = 8;
                        }

                        server.CommitChanges();
                    }
                    else
                    {
                        errorCode = IISErrorCode.AppExists;
                    }
                }
                else
                {
                    errorCode = IISErrorCode.SiteNotFound;
                }
            }
            catch (Exception ex)
            {
                errorCode = IISErrorCode.Unknown;
                Console.WriteLine(ex.Message);
            }

            return errorCode;
        }

        public int RemoveApp(string siteName, string virtualPath)
        {
            int errorCode = IISErrorCode.Succeed;

            try
            {
                var server = new ServerManager();

                //先找站点
                Site targetSite = null;
                foreach (var site in server.Sites)
                {
                    if (site.Name.EqualsEx(siteName))
                    {
                        targetSite = site;
                        break;
                    }
                }

                if (targetSite != null)
                {
                    //再找应用
                    Microsoft.Web.Administration.Application targetApp = null;
                    foreach (var app in targetSite.Applications)
                    {
                        if (app.Path.Equals(virtualPath))
                        {
                            targetApp = app;
                            break;
                        }
                    }

                    //存在才删除
                    if (targetApp != null)
                    {
                        targetSite.Applications.Remove(targetApp);
                        server.CommitChanges();
                    }
                    else
                    {
                        errorCode = IISErrorCode.AppNotFound;
                    }
                }
                else
                {
                    errorCode = IISErrorCode.SiteNotFound;
                }
            }
            catch (Exception ex)
            {
                errorCode = IISErrorCode.Unknown;
                Console.WriteLine(ex.Message);
            }

            return errorCode;
        }
        #endregion

        #region 域名绑定
        /// <summary>
        /// 通过站点名获取绑定的域名列表
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public IList<IISDomain> GetSiteDomains(string siteName)
        {
            ServerManager server = new ServerManager();
            IList<IISDomain> domains = new List<IISDomain>();
            foreach (Site site in server.Sites)
            {
                if (site.Name == siteName.Trim())
                {
                    foreach (var bind in site.Bindings)
                    {
                        var domain = new IISDomain();
                        domain.BindingInformation = bind.BindingInformation;
                        domain.Host = bind.Host;
                        domains.Add(domain);
                    }
                }
            }

            return domains;

        }

        /// <summary>
        /// 添加绑定域名
        /// </summary>
        /// <param name="siteName">站点名</param>
        /// <param name="domainUrl">域名URL</param>
        /// <param name="port">端口</param>
        /// <param name="https">默认为http</param>
        /// <returns></returns>
        public int AddSiteDomain(string siteName, string domainUrl, int port = 80, bool https = false)
        {
            int errorCode = IISErrorCode.Succeed;
            try
            {
                var server = new ServerManager();
                Site site = null;
                //找指定站点
                foreach (Site s in server.Sites)
                {
                    if (s.Name.EqualsEx(siteName))
                    {
                        site = s;
                        break;
                    }
                }
                if (site != null)
                {
                    bool exists = false;
                    foreach (var bind in site.Bindings)
                    {
                        if (https)
                        {
                            if (bind.Host.EqualsEx(domainUrl) && bind.Protocol.EqualsEx("https") && bind.EndPoint.Port == port)
                            {
                                exists = true;
                                break;
                            }
                        }
                        else
                        {
                            if (bind.Host.EqualsEx(domainUrl) && bind.Protocol.EqualsEx("http") && bind.EndPoint.Port == port)
                            {
                                exists = true;
                                break;
                            }
                        }

                    }
                    if (!exists)
                    {
                        site.Bindings.Add(string.Format("*:{1}:{0}", domainUrl, port), https ? "https" : "http");
                        server.CommitChanges();
                        //site.ServerAutoStart();
                    }
                }
                else
                {
                    errorCode = IISErrorCode.SiteNotFound;
                }
            }
            catch (Exception ex)
            {
                errorCode = IISErrorCode.Unknown;
                throw new Exception(ex.Message);
            }
            return errorCode;
        }

        public Site GetSite(string siteName)
        {
            var code = SiteExists(siteName);
            Site site = null;
            if (code == IISErrorCode.SiteExists)
            {
                var server = new ServerManager();

                //找指定站点
                foreach (Site s in server.Sites)
                {
                    if (s.Name.EqualsEx(siteName))
                    {
                        site = s;
                        break;
                    }
                }
            }
            else
            {
                throw new Exception("IISErrorCode:" + code);
            }

            return site;
        }
        #endregion

        #region Utilities
        public int SiteExists(string siteName)
        {
            int errorCode = IISErrorCode.SiteNotFound;

            try
            {
                var server = new ServerManager();

                //找指定站点
                foreach (Site site in server.Sites)
                {
                    if (site.Name.EqualsEx(siteName))
                    {
                        errorCode = IISErrorCode.SiteExists;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                errorCode = IISErrorCode.Unknown;
                Console.WriteLine(ex.Message);
            }

            return errorCode;
        }



        public int SetCertificate(string siteName, string sslHash)
        {
            int errorCode = IISErrorCode.Succeed;

            try
            {
                var server = new ServerManager();

                //找指定站点
                Site targetSite = null;
                foreach (var site in server.Sites)
                {
                    if (site.Name.EqualsEx(siteName))
                    {
                        targetSite = site;
                        break;
                    }
                }

                //存在才后续处理
                if (targetSite != null)
                {
                    foreach (var binding in targetSite.Bindings)
                    {
                        //只处理https协议
                        if (binding.Protocol.EqualsEx("https"))
                        {
                            binding.CertificateHash = HexString2Bytes(sslHash);
                            server.CommitChanges();
                            break;
                        }
                    }
                }
                else
                {
                    errorCode = IISErrorCode.SiteNotFound;
                }
            }
            catch (Exception ex)
            {
                errorCode = IISErrorCode.Unknown;
                Console.WriteLine(ex.Message);
            }

            return errorCode;
        }

        public int SetPort(string siteName, int httpPort, int httpsPort)
        {
            int errorCode = IISErrorCode.Succeed;

            try
            {
                var server = new ServerManager();

                //找指定站点
                Site targetSite = null;
                foreach (var site in server.Sites)
                {
                    if (site.Name.EqualsEx(siteName))
                    {
                        targetSite = site;
                        break;
                    }
                }

                //存在才后续处理
                if (targetSite != null)
                {
                    foreach (var binding in targetSite.Bindings)
                    {
                        if (httpPort > 0 && binding.Protocol.EqualsEx("http"))
                        {
                            binding.BindingInformation = string.Format("*:{0}:", httpPort);
                        }

                        if (httpsPort > 0 && binding.Protocol.EqualsEx("https"))
                        {
                            binding.BindingInformation = string.Format("*:{0}:", httpsPort);
                        }
                    }
                    server.CommitChanges();
                }
                else
                {
                    errorCode = IISErrorCode.SiteNotFound;
                }
            }
            catch (Exception ex)
            {
                errorCode = IISErrorCode.Unknown;
                Console.WriteLine(ex.Message);
            }

            return errorCode;
        }
        #endregion
    }
}
