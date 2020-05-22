using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryTask
{
    public class worker_async
    {
		CancellationTokenSource _ct;
		int _max;
		int _delay;

		public worker_async(CancellationTokenSource ct, int max, int delay)
		{
			_ct = ct;
			_max = max;
			_delay = delay;
		}

		public async Task start()
		{
			await Task.Factory.StartNew(DoWork);
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
