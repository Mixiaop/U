using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace U.Reflection
{
    public class BuildManagerAssemblyFinder : IAssemblyFinder
    {
        public static BuildManagerAssemblyFinder Instance { get; private set; }

        static BuildManagerAssemblyFinder()
        {
            Instance = new BuildManagerAssemblyFinder();
        }

        public List<Assembly> GetAllAssemblies()
        {
            return BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
        }
    }
}
