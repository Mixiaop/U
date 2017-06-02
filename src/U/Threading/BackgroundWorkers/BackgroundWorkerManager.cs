using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U.Dependency;

namespace U.Threading.BackgroundWorkers
{
    /// <summary>
    /// Implements <see cref="IBackgroundWorkerManager"/>
    /// </summary>
    public class BackgroundWorkerManager : RunnableBase, IBackgroundWorkerManager, ISingletonDependency, IDisposable
    {
        private readonly List<IBackgroundWorker> _backgroundWorkers;
        public BackgroundWorkerManager() {
            _backgroundWorkers = new List<IBackgroundWorker>();
        }

        public override void Start()
        {
            base.Start();

            _backgroundWorkers.ForEach(job => job.Start());
        }

        public override void Stop()
        {
            _backgroundWorkers.ForEach(job => job.Stop());

            base.WaitToStop();
        }

        public override void WaitToStop()
        {
            _backgroundWorkers.ForEach(job => job.WaitToStop());

            base.WaitToStop();
        }

        public void Add(IBackgroundWorker worker)
        {
            _backgroundWorkers.Add(worker);

            if (IsRunning)
            {
                worker.Start();
            }
        }

        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            _backgroundWorkers.Clear();
        }
    }
}
