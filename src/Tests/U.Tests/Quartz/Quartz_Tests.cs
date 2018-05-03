using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quartz;
using U.Quartz;
using U.Dependency;
using U.Logging;

namespace U.Tests.Quartz
{
    [TestClass]
    public class Quartz_Tests
    {
        [TestMethod]
        public void Tests() {
            var manager = UPrimeEngine.Instance.Resolve<IQuartzScheduleJobManager>();
            manager.ScheduleAsync<MyLogJob>(
            job =>
            {
                job.WithIdentity("MyLogJobIdentity", "MyGroup")
                    .WithDescription("A job to simply write logs.");
            },
            trigger =>
            {
                trigger.StartNow()
                    .WithSimpleSchedule(schedule =>
                    {
                        schedule.RepeatForever()
                            .WithIntervalInSeconds(5)
                            .Build();
                    });
            });
        }
    }

    public class MyLogJob : JobBase, ITransientDependency
    {
        public override void Execute(IJobExecutionContext context)
        {
            LogHelper.Logger.Information("MyLogJob.Excecute");
        }
    }
}
