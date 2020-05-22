using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryTask
{
    public class Worker
    {
		CancellationTokenSource _ct;
		int _max;
		int _delay;

		public Worker(CancellationTokenSource ct, int max, int delay)
		{
			_ct = ct;
			_max = max;
			_delay = delay;
		}

		public void start()
		{
			Task.Factory.StartNew(DoWork);
		}

		private void DoWork()
		{
            for (int i = 0; i < _max; i++)
			{
				Task.Delay(_delay);
                if (_ct.IsCancellationRequested)
					break;
			}

		}
	}
}
