using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Collections._4.ProducerConsumerCollections_Concurrent_Queues_Stacks_Bags
{
	//------------CoQUEUES,CoSTACK,CoBag-------------Producer-Consumer-Collection----
	//removing items in concurreny is hard
	//ConcurrentBag Helps performance

	//Here in Buy & Sell App, the sample uses separate thread for each SalesPerson
	//Everytime SalesPerson buys or sells TShirts, the stock level is intantly updated in real time.

	//Now we want logging to log th details of every trade to central data store.these are permanent records so need not be real time.
	//say logging is very slow due to slow database connection.so, SalesPerson thread can't be tied up for logging.
	//SOLUTION: have a QUEUE of trades to be logged.Everytime a sale is made then each SalesPerson thread immediately posts those trade details to queue.
	//On this QUEUE, keep 2 dedicated threads working to process each trade on QUEUE and log it to datastore.


	public class Coll_CoQu_CoSt_CoBg
	{
		public static void Run()
		{
			StockController controller = new StockController();
			TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);
			StaffRecords staffLogs = new StaffRecords();//represents data store
			LogTradesQueue tradesQueue = new LogTradesQueue(staffLogs);//Queue

			SalesPerson[] staff = 
			{
				new SalesPerson("Jack"),
				new SalesPerson("Anna"),
				new SalesPerson("Kim"),
				new SalesPerson("Mike")
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

			//Here SalesPerson threads ENQUEUE items, Logging threads DEQUEUE items.they are completely different threads for Adding & Removing.
			//So, ConcurrentBag<T> is not good fit in this project.but demo is in "LogTradesQueue"

			Task.WaitAll(salesTasks.ToArray());
			tradesQueue.SetNoMoreTrades();
			Task.WaitAll(loggingTasks);

			controller.DisplayStock();
			staffLogs.DisplayCommissions(staff);
		}
	}
}