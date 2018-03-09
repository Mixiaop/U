using System;
using System.Threading.Tasks;
using U.Threading.BackgroundWorkers;

namespace U.BackgroundJobs
{
    /// <summary>
    /// Defines interface used to manage all background jobs.
    /// 定义接口用于管理所有的后台作业
    /// </summary>
    public interface IBackgroundJobManager : IBackgroundWorker
    {

        /// <summary>
        /// 添加周期性（重复）的任务（如果有任务标识则返回）
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <typeparam name="TArgs"></typeparam>
        /// <param name="args"></param>
        /// <param name="timeZone"></param>
        /// <param name="cronExpression">cron expression</param>
        void AddRecurringJob<TJob, TArgs>(TArgs args, string cronExpression, TimeZoneInfo timeZone = null) where TJob : IBackgroundJob<TArgs>;

        /// <summary>
        /// 添加有计划时间的任务进入队列，并返回任务标识
        /// </summary>
        /// <typeparam name="TJob">类型</typeparam>
        /// <typeparam name="TArgs">参数类型</typeparam>
        /// <param name="args">参数</param>
        /// <param name="enqueueAt">计划时间</param>
        /// <param name="priority">优先级</param>
        /// <returns></returns>
        string Schedule<TJob, TArgs>(TArgs args, DateTime enqueueAt, BackgroundJobPriority priority = BackgroundJobPriority.Normal)
            where TJob : IBackgroundJob<TArgs>;

        /// <summary>
        /// 添加有计划时间的job进入队列，并返回job标识
        /// </summary>
        /// <typeparam name="TJob">类型</typeparam>
        /// <typeparam name="TArgs">参数类型</typeparam>
        /// <param name="args">参数</param>
        /// <param name="enqueueAt">计划时间</param>
        /// <param name="priority">优先级</param>
        /// <returns></returns>
        Task ScheduleAsync<TJob, TArgs>(TArgs args, DateTime enqueueAt, BackgroundJobPriority priority = BackgroundJobPriority.Normal)
            where TJob : IBackgroundJob<TArgs>;

        /// <summary>
        /// 马上执行任务（进入队列）
        /// </summary>
        /// <typeparam name="TJob">类型</typeparam>
        /// <typeparam name="TArgs">参数类型</typeparam>
        /// <param name="args">参数</param>
        /// <param name="priority">优先级</param>
        /// <param name="delay">延迟时间</param>
        /// <returns></returns>
        string Enqueue<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>;

        
        /// <summary>
        /// 马上执行任务（进入队列），并返回任务标识
        /// </summary>
        /// <typeparam name="TJob">类型</typeparam>
        /// <typeparam name="TArgs">参数类型</typeparam>
        /// <param name="args">参数</param>
        /// <param name="priority">优先级</param>
        /// <param name="delay">延迟时间</param>
        /// <returns></returns>
        Task EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>;

        /// <summary>
        /// 通过任务Id删除任务（RecurringJob除外）
        /// </summary>
        /// <param name="jobId"></param>
        void Delete(string jobId);

        /// <summary>
        /// 通过任务Id检查是否存在于任务列表中（计划、处理中）
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        bool Exists(string jobId);
    }
}
