using System;
using System.Threading;

namespace ThreadTestApp
{
	public class ExampleTimer
	{
		public void Main()
		{
			var message = "Mom";
			TimerCallback tm = new TimerCallback(Count);
			new Timer(tm, message, 0, 500);

			Console.ReadLine();
		}
		private void Count(object obj)
		{
			var message = (string)obj;
			Console.WriteLine(message);
		}
	}
}