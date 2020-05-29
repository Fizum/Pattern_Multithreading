using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryTask
{
    public class WorkerProgressAsync
    {
        CancellationTokenSource _ct;
        IProgress<int> _progress;
        int _max;
        int _delay;
        Semaphore _sem;

        public WorkerProgressAsync(Semaphore sem, CancellationTokenSource ct, int max, int delay, IProgress<int> progress)
        {
            _ct = ct;
            _max = max;
            _delay = delay;
            _progress = progress;
            _sem = sem;
        }

        public async Task start()
        {
            await Task.Factory.StartNew(DoWork);
        }

        private void DoWork()
        {
            _sem.WaitOne();
            for (int i = 0; i < _max; i++)
            {
                NotifyProgress(_progress, i);
                Task.Delay(_delay);

                if (_ct.IsCancellationRequested)
                    break;
            }
            _sem.Release();
        }

        private void NotifyProgress(IProgress<int> progress, int i)
        {
            progress.Report(i);
        }
    }
}
