using System;
using System.Threading.Tasks;
using Hangfire;
using U.BackgroundJobs;
using U.Threading.BackgroundWorkers;
using U.Logging;
using HangfireBackgroundJob = Hangfire.BackgroundJob;
using U.Hangfire.Startup.Configuration;

namespace U.Hangfire
{
    public class HangfireBackgroundJobManager : BackgroundWorkerBase, IBackgroundJobManager
    {
        private readonly IUHangfireConfiguration _hangfireConfiguration;
        //private readonly IHangfireDataProvider _hangfireDataProvider;
        public HangfireBackgroundJobManager(
            IUHangfireConfiguration hangfireConfiguration
            )
        {
            _hangfireConfiguration = hangfireConfiguration;
            //_hangfireDataProvider = hangfireDataProvider;
        }

        public override void Start()
        {
            base.Start();

            if (_hangfireConfiguration.Server == null)
            {
                _hangfireConfiguration.Server = new BackgroundJobServer();
            }
        }

        public override void WaitToStop()
        {
            if (_hangfireConfiguration.Server != null)
            {
                try
                {
                    _hangfireConfiguration.Server.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString(), ex);
                }
            }

            base.WaitToStop();
        }

        public void AddRecurringJob<TJob, TArgs>(TArgs args, string cronExpression, TimeZoneInfo timeZone = null) where TJob : IBackgroundJob<TArgs>
        {
            RecurringJob.AddOrUpdate<TJob>(job => job.Execute(args), cronExpression, timeZone);
        }

        public string Schedule<TJob, TArgs>(TArgs args, DateTime enqueueAt, BackgroundJobPriority priority = BackgroundJobPriority.Normal)
           where TJob : IBackgroundJob<TArgs> {
               enqueueAt = DateTime.SpecifyKind(enqueueAt, DateTimeKind.Local);
               DateTimeOffset enqueueAtOffset = enqueueAt;
            
               string jobId = HangfireBackgroundJob.Schedule<TJob>(job => job.Execute(args), enqueueAtOffset);
               return jobId;
        }

        public Task ScheduleAsync<TJob, TArgs>(TArgs args, DateTime enqueueAt, BackgroundJobPriority priority = BackgroundJobPriority.Normal)
            where TJob : IBackgroundJob<TArgs>
        {

            Schedule<TJob, TArgs>(args, enqueueAt, priority);

            return Task.FromResult(0);
        }

        public string Enqueue<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null) where TJob : IBackgroundJob<TArgs>
        {
            string jobId = HangfireBackgroundJob.Enqueue<TJob>(job => job.Execute(args));

            return jobId;
        }

        public Task EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null) where TJob : IBackgroundJob<TArgs>
        {
            Enqueue<TJob, TArgs>(args, priority, delay);

            return Task.FromResult(0);
        }

        public void Delete(string jobId) {
            HangfireBackgroundJob.Delete(jobId);
        }

        public bool Exists(string jobId) {
            //return _hangfireDataProvider.Exists(jobId);
            throw new NotImplementedException();
        }
    }
}
