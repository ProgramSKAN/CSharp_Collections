using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collections._4.ProducerConsumerCollections_Concurrent_Queues_Stacks_Bags
{
	//In this Project, Using ConcurrentQueue or ConcurrentStack doesn't matter because order of logging to datastore don't matter.Just logging enough.
	//ConcurrentBag gives best performance when order doesn't matter.But is efficient only if same threads are adding and removing items from Bag.
	//internall ConcurrentBag works by as far as possible,keeping a separate collection for each thread.so it can largely avoid any thread synchronization.
	public class LogTradesQueue
	{
		//private ConcurrentQueue<Trade> _tradesToLog = new ConcurrentQueue<Trade>(); //OR
		//private ConcurrentStack<Trade> _tradesToLog = new ConcurrentStack<Trade>(); //OR
		//private ConcurrentBag<Trade> _tradesToLog = new ConcurrentBag<Trade>();
		//------OR----OR-----OR--------------------------------------------
		/* CONCURRENTQUEUE > Enqueue() , TryDequeue()
		 * CONCURRENTSTACK > Push() , TryPop()
		 * CONCURRENTBAG > Add() , TryTake()
		 * 
		 * To abstract different methods for same purpose, use IProducerConsumerCollection<T>
		 * IProducerConsumerCollection<T> > TryAdd() , TryTake()      >Add() don't need TryAdd() since Add always works.but named as TryAdd() for future proof for future collections
		 * so,Now if Type is changed from CoQueue to CoStack, no need to change methods.
		 */

		//private IProducerConsumerCollection<Trade> _tradesToLog = new ConcurrentQueue<Trade>();
		//private IProducerConsumerCollection<Trade> _tradesToLog = new ConcurrentStack<Trade>();
		private IProducerConsumerCollection<Trade> _tradesToLog = new ConcurrentBag<Trade>();

		private readonly StaffRecords _staffLogs;
		private bool _workingDayComplete;
		public LogTradesQueue(StaffRecords staffLogs)
		{
			_staffLogs = staffLogs;
		}
		public void SetNoMoreTrades() => _workingDayComplete = true;

		//public void QueueTradeForLogging(Trade trade) => _tradesToLog.Enqueue(trade);//there is not TryEnqueue() because you can always Enqueue an item no matter what the queue state is in.so, Enqeue() never fails
		//OR
		//public void QueueTradeForLogging(Trade trade) => _tradesToLog.Push(trade);
		//OR
		//public void QueueTradeForLogging(Trade trade) => _tradesToLog.Add(trade);
		//----OR----OR----OR-----------------
		public void QueueTradeForLogging(Trade trade) => _tradesToLog.TryAdd(trade);


		public void MonitorAndLogTrades()
		{
			while (true)
			{
				Trade nextTrade;
				//bool done = _tradesToLog.TryDequeue(out nextTrade);//its TryDequeue() not Dequeue() because other threads might have already emptied the queue.to check such scenarios
				//OR
				//bool done = _tradesToLog.TryPop(out nextTrade);
				//OR
				//bool done = _tradesToLog.TryTake(out nextTrade);
				//----OR----OR----OR-----------------
				bool done = _tradesToLog.TryTake(out nextTrade);

				if (done)
				{
					_staffLogs.LogTrade(nextTrade);
					Console.WriteLine(
						$"Processing transaction from {nextTrade.Person.Name}");
				}
				else if (_workingDayComplete)
				{
					Console.WriteLine("No more sales to log - exiting");
					return;
				}
				else
				{
					//in SINGLE THREADED : if QUEUE is empty, it means you have finished.
					//This is CONCURRENT : so, you don't know if you've finished because other threads might still add stuff.
					Console.WriteLine("No transactions available");
					Thread.Sleep(500);//This is polling the Queue for more data for every 500ms.Bad practice.
				}
			}
		}
	}
}
