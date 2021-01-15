using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Collections._5.BlockingCollection_and_ConsumingEnumerable
{
	//-------BLOCKING COLLECTION----------------
	//it is use for the problem in "LogTradesQueue">"MonitorAndLogTrades()" where polling Queue is a badpractice.

	
	public class Coll_BlockingColl
	{
		public static void Run()
		{
			StockController controller = new StockController();
			TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);
			StaffRecords staffLogs = new StaffRecords();
			LogTradesQueue tradesQueue = new LogTradesQueue(staffLogs);

			SalesPerson[] staff =
			{
				new SalesPerson("Jack"),
				new SalesPerson("Anna"),
				new SalesPerson("Kim"),
				new SalesPerson("Julie")
			};
			List<Task> salesTasks = new List<Task>();
			foreach (SalesPerson person in staff)
			{
				salesTasks.Add(
					Task.Run(() => person.Work(workDay, controller, tradesQueue)));
			}

			Task[] loggingTasks =
			{
				Task.Run(() => tradesQueue.MonitorAndLogTrades()),
				Task.Run(() => tradesQueue.MonitorAndLogTrades())
			};

			Task.WaitAll(salesTasks.ToArray());
			tradesQueue.SetNoMoreTrades();
			Task.WaitAll(loggingTasks);

			controller.DisplayStock();
			staffLogs.DisplayCommissions(staff);
		}
	}
}