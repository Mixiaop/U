using System.Web;
using StackExchange.Profiling;
using U.BaseService.Profiler.Configuration;

namespace U.BaseService.Profiler
{
    public class ProfilerManager
    {

        public static MiniProfiler Start()
        {
            var config = ProfilerConfigs.GetConfig();
            if (config.Apply)
                return MiniProfiler.Start();
            else return null;
        }

        public static void Stop()
        {
            var config = ProfilerConfigs.GetConfig();
            if (config.Apply)
                MiniProfiler.Stop();
        }


        public static IHtmlString RenderIncludes()
        {
            var config = ProfilerConfigs.GetConfig();
            if (config.Apply)
                return MiniProfiler.RenderIncludes();
            else
                return new HtmlString("");
        }
    }
}
