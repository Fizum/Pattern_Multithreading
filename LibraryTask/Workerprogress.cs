using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryTask
{
    public class Workerprogress
    {
		CancellationTokenSource _ct;
		IProgress<int> _progress;
		int _max;
		int _ritardo;

		public Workerprogress(CancellationTokenSource ct, int max, int ritardo, IProgress<int> progress)
		{
			_ct = ct;
			_max = max;
			_ritardo = ritardo;
			_progress = progress;
		}

		public void start()
		{
			Task.Factory.StartNew(DoWork);
		}

		private void DoWork()
		{
			
			for (int i = 0; i < _max; i++)
			{
				NotifyProgress(_progress, i);
				Task.Delay(_ritardo);
				if (_ct.IsCancellationRequested)
				{
					break;
				}
			}

		}

		private void NotifyProgress(IProgress<int> progress, int i)
		{
			progress.Report(i);
		}
	}
}
