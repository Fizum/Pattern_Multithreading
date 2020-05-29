using LibraryTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pattern_Multithreading
{
	/// <summary>
	/// Logica di interazione per MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		CancellationTokenSource ct;
        Semaphore sem = new Semaphore(1, 2);

		private async void Btn_avvia_Click(object sender, RoutedEventArgs e)
		{
			ct = new CancellationTokenSource();
            //worker_async wrk = new worker_async(ct, 10, 1000);
            IProgress<int> progress = new Progress<int>(UpdateUI);
            WorkerProgressAsync wrk = new WorkerProgressAsync(sem, ct, 100, 2000, progress);
			
			//IProgress<int> progress = new Progress<int>(UpdateUI);
			//Workerprogress wrk = new Workerprogress(ct, 10, 1000, progress);
			await wrk.start();
			MessageBox.Show("Mi dimentico del thread secondario e non attendo il thread secondario per visualizzare questo messaggio");
		}

		private void UpdateUI(int i)
		{
			Lbl_visualizza.Content = i.ToString();
		}

		private void Btn_ferma_Click(object sender, RoutedEventArgs e)
		{
            if (ct != null)
				ct.Cancel();
		}
	}
}
