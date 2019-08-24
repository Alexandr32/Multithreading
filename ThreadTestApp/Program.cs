namespace ThreadTestApp
{
	internal class Program
	{
		private static void Main()
		{
			//var fib = new Fib(true);
			//fib.Main();
			//System.Console.ReadKey();
			var test = new ThreadTest();
			test.Main(7);


			//System.Diagnostics.Process.Start();
			//System.Threading.Thread.Sleep(50);
			//System.Threading.Thread.SpinWait(50);
		}
	}
}
