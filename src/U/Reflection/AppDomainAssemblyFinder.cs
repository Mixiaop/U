using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace U.Reflection
{
    /// <summary>
    /// AppDomain应用程序集查询器，这里主要指IIS程序池
    /// </summary>
    public class AppDomainAssemblyFinder : IAssemblyFinder
    {
        public static AppDomainAssemblyFinder Instance { get; private set; }

        static AppDomainAssemblyFinder()
        {
            Instance = new AppDomainAssemblyFinder();
        }

        public List<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
    }
}
