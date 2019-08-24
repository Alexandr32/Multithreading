using System;
using System.Threading;

namespace ThreadTestApp
{
	public class ExampleSynchronizingThreadsc
	{
		[ThreadStatic]
		public static int Resurses = 0;
		private static object _locker = new object();

		public void Main()
		{
			for (int i = 1; i <= 5; i++)
			{
				Thread myThread = new Thread(Count)
				{
					Name = i.ToString()
				};
				myThread.Start();
			}

			Console.ReadLine();
		}

		private void Count()
		{
			lock (_locker)
			{
				Resurses = 1;
				for (var i = 1; i < 9; i++)
				{
					Console.WriteLine($"Name Thread =  {Thread.CurrentThread.Name} Resurses = {Resurses}");
					Resurses++;
					Thread.Sleep(100);
				}
			}
		}
	}
}