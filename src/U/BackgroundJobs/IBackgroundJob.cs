
namespace U.BackgroundJobs
{
    /// <summary>
    /// Defines interface of a background job
    /// 定义一个后台作业接口
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public interface IBackgroundJob<in TArgs>
    {
        /// <summary>
        /// Execute the job with the <see cref="args"/>
        /// 执行作业
        /// </summary>
        /// <param name="args">job arguments</param>
        void Execute(TArgs args);
    }
}
