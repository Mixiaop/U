using Quartz;
using System;
using System.Threading.Tasks;
using U.Threading.BackgroundWorkers;

namespace U.Quartz
{
    public interface IQuartzScheduleJobManager : IBackgroundWorker
    {
        /// <summary>
        /// 计划一项任务来执行
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="configureJob"></param>
        /// <param name="configureTrigger"></param>
        /// <returns></returns>
        Task ScheduleAsync<TJob>(Action<JobBuilder> configureJob, Action<TriggerBuilder> configureTrigger) where TJob : IJob;
    }
}
