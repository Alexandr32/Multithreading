using System;

namespace ThreadTestApp
{
	public class SyncDelegate
	{
		public void Main()
		{
			Action<string, string> sendAction = TestMethod;
			var res = sendAction?.BeginInvoke("Roman", "Muratov", OnSendCompleted, sendAction);
			if (res.IsCompleted)
			{
				
			}
			sendAction.EndInvoke(res);
			Console.ReadKey();
		}

		private void TestMethod(string name, string lastName)
		{
			Console.WriteLine(name);
			Console.WriteLine(lastName);
		}

		private void OnSendCompleted(IAsyncResult result)
		{
			var sendAction = (Action<string, string>)result.AsyncState;

			sendAction.EndInvoke(result);
		}
	}
}