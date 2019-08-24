using System;
using System.Numerics;

namespace ThreadTestApp
{
	public class Fib
	{
		private bool _isRec;
		public Fib(bool isRec)
		{
			_isRec = isRec;
		}
		public void Main()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			for (int i = 1; i <= 9999; i++)
			{
				DateTime startTime = DateTime.Now;
				var result = _isRec ? FibR(i) : FibC(i);
				var time = (DateTime.Now - startTime).TotalSeconds;
				Console.WriteLine($"Fib({i}) - {result} за {time}");
			}
		}
		private BigInteger FibC(int n)
		{
			BigInteger a0 = 0, a1 = 1, temp = 0;
			for (var i = 0; i <= n; i++)
			{
				temp = a0 + a1;
				a0 = a1;
				a1 = temp;
			}
			return temp;
		}
		private BigInteger FibR(int n) => (n == 1 || n == 2) ? 1 : FibR(n - 1) + FibR(n - 2);
	}
}