using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collections._5.BlockingCollection_and_ConsumingEnumerable
{
	//-------BLOCKING COLLECTION------
	//---_tradesToLog.GetConsumingEnumerable()-------
	public class LogTradesQueue
	{
		//private IProducerConsumerCollection<Trade> _tradesToLog = new ConcurrentQueue<Trade>(); //add BlockingCollection to wake MonitorAndLogTrades() as soon as item is added.
		private BlockingCollection<Trade> _tradesToLog = new BlockingCollection<Trade>(
							//new ConcurrentQueue<Trade>()  //default
							//you can use
							//new ConcurrentStack<Trade>()    //new ConcurrentBag<Trade>()
							); //BLOCKINGCOLLECTION is not a collection in its own right.it just encapsulates Producer-Consumer collections and provides additional services to collection.

		private readonly StaffRecords _staffLogs;

		//private bool _workingDayComplete;
		public LogTradesQueue(StaffRecords staffLogs)
		{
			_staffLogs = staffLogs;
		}

		//public void SetNoMoreTrades() => _workingDayComplete = true;  //not required Since there is BlockingCollection
		public void SetNoMoreTrades() => _tradesToLog.CompleteAdding();//this should be called when no more items will be added to Collection.
		
		public void QueueTradeForLogging(Trade trade) => _tradesToLog.TryAdd(trade);

		public void MonitorAndLogTrades()
		{
            /*while (true)
			{
				Trade nextTrade;
				bool done = _tradesToLog.TryTake(out nextTrade);
				if (done)
				{
					_staffLogs.LogTrade(nextTrade);
					Console.WriteLine($"Processing transaction from {nextTrade.Person.Name}");
				}
				else if (_workingDayComplete)
				{
					Console.WriteLine("No more sales to log - exiting");
					return;
				}
				else
				{
					Console.WriteLine("No transactions available");
					Thread.Sleep(500);   //Polling is Bad Practice  //if there are not logs in queue we should not finish because logs can be there in future.
				}
			}*/
            //PROBLEM: Consuming an empty Queue when you don't know more items will be added
            //SOLUTION: Wake as soon as the item is added into queue not after fixed 500ms like polling. > BlockingCollection<T>   (Avoid Polling)
            
			while (true)
            {
                try
                {
					Trade nextTrade = _tradesToLog.Take();//it is Take() not TryTake() because in BlockingCollection<T> if you try to Take an item and the collection is empty, then the Take() method will wait, blocking the thread until an item is added to the collection to Take.
					_staffLogs.LogTrade(nextTrade);
                    Console.WriteLine($"Processing transaction from {nextTrade.Person.Name}");
                }
                catch (InvalidOperationException ex)
                {
					//if BlockingCollection<T> either gets informed or has already been infromed via call to CompleteAddin() method that no more items are expected
					//the Take() throws exception when CompleteAdding() called
					Console.WriteLine(ex.Message);
					return;
                }
            }

			//OR The above while loop can be simplified to foreach loop.
			/*foreach(Trade nextTrade in _tradesToLog.GetConsumingEnumerable())
            {
				_staffLogs.LogTrade(nextTrade);
				Console.WriteLine($"Processing transaction from {nextTrade.Person.Name}");
			}*/
			//foreach by definition its read-only(ie, just enumeration)
			//but here its not, though it looks enumeration, but actually you are CONSUMING(removing them as u go).

			//NOTE:: Don't Use
			//foreach (Trade nextTrade in _tradesToLog) { }//it is SNAPSHOT //it will be all 0's because it just copies the queue before any trades and enumerates the copy
		}
	}
}
