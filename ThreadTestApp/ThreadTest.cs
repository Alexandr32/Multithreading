using System;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using static System.Console;
namespace ThreadTestApp
{
	public class ThreadTest
	{
		public void Main(int i)
		{
			switch (i)
			{
				case 0:
					ThreadSimpleTest();
					break;
				case 1:
					ParametricThreadTest();
					break;
				case 2:
					MultyThreadTest();
					break;
				case 3:
					SynchronizationPrimitivesTest();
					break;
				case 4:
					new ExampleSynchronizingThreadsc().Main();
					break;
				case 5:
					ThreadPoolTest();
					break;
				case 6:
					new ExampleTimer().Main();
					break;
				case 7:
					new SyncDelegate().Main();
					break;
				default:
					WriteLine("Нет такого варианта");
					ReadKey();
					break;
			}
		}

		private void ThreadSimpleTest()
		{
			WriteLine("Главный поток id={0} запущен", Thread.CurrentThread.ManagedThreadId);

			var thread = new Thread(FirstThreadMethod); //new Thread(new ThreadStart(FirstThreadMethod));               

			thread.Start();
			//thread.Join();

			#region Акт 2

			thread.Name = "Вторичный поток";
			thread.Priority = ThreadPriority.AboveNormal;
			thread.IsBackground = true;

			#endregion

			#region Акт 3

			WriteLine("Ожидаем фоновый поток");
			if (thread.Join(3000))
				WriteLine("Потоки синхронизировались");
			else
			{
				WriteLine("Время ожидания завершилось. Потоки не синхронизированы");
				thread.Abort();
			}

			#endregion

			//ReadLine();
			WriteLine("Главный поток id={0} остановлен", Thread.CurrentThread.ManagedThreadId);
			ReadLine();
		}

		private void FirstThreadMethod()
		{
			WriteLine("Поток id={0} {1} запущен", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);

			Thread.Sleep(5000);

			WriteLine("Поток id={0} {1} завершён", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);
		}

		private string sds;
		private void ParametricThreadTest()
		{
			//var thread = new Thread(ParameterThreadMethod);
			//thread.Start("Тестовые данные для потока");

			//for (int i = 0; i < 10; i++)
			//{
			//	var tempI = i;
			//	var thread = new Thread(() => PrintDataToConsole($" {tempI}", true));
			//	thread.Start();
			//}

			var thread = new Thread(ParameterStructThreadMethod);
			thread.Start(new MyStruct { Id = 3, Name = "Roman" });

			ReadLine();
		}

		private void ParameterThreadMethod(object threadParameter)
		{
			if (!(threadParameter is string str)) return;

			WriteLine(str);
		}

		private void PrintDataToConsole(string msg, bool isTrue)
		{
			WriteLine(msg);
		}

		private void ParameterStructThreadMethod(object msg)
		{
			if (!(msg is MyStruct obj)) return;

			WriteLine(obj.Name);
			WriteLine(obj.Id);
		}

		private struct MyStruct
		{
			public string Name { get; set; }
			public int Id { get; set; }
		}


		private void MultyThreadTest()
		{
			for (var i = 1; i <= 10; i++)
			{
				var thread = new Thread(MultyThreadTestMethod);
				thread.Start();
			}

			ReadKey();
		}

		private readonly object _syncRoot = new object();
		private void MultyThreadTestMethod()
		{
			#region То, что находится внутри конструкции lock
			//bool lockTaken = false;
			//Monitor.Enter(_syncRoot, ref lockTaken);
			//Monitor.Enter(_syncRoot);
			//Monitor.Wait(_syncRoot, 1000);
			//try
			//{

			//}
			//finally
			//{
			//    Monitor.Exit(_syncRoot);
			//} 
			#endregion

			lock (_syncRoot)
			{
				Write("Thread id={0}:", Thread.CurrentThread.ManagedThreadId);
				for (var i = 1; i <= 10; i++)
				{
					Write("{0},", i);
				}
				WriteLine();
			}
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		private void MultyThreadTestSyncMethod()
		{

		}

		[Synchronization]
		private class MySyncClass : ContextBoundObject
		{

		}


		//private AutoResetEvent _event = new AutoResetEvent(false);
		private ManualResetEvent _event = new ManualResetEvent(false);

		private void SynchronizationPrimitivesTest()
		{
			var thread = new Thread(SynchronizationPrimitivesTestThreadMethod);
			var thread2 = new Thread(SynchronizationPrimitivesTestThreadMethod);
			thread.Start();
			thread2.Start();

			ReadLine();
			_event.Set();


			ReadLine();
			_event.Set();

			ReadLine();
		}

		private void SynchronizationPrimitivesTestThreadMethod()
		{
			WriteLine("Вторичный поток запущен...");
			_event.WaitOne();

			WriteLine("Вторичный поток завершён");
		}
		

		public void ThreadPoolTest()
		{
			ThreadPool.SetMinThreads(2, 2);
			ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);

			for (var i = 1; i <= 50; i++)
			{
				ThreadPool.QueueUserWorkItem(o => MultyThreadTestMethod());
			}

			ReadKey();
		}
	}
}