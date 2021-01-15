using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Collections._4.ProducerConsumerCollections_Concurrent_Queues_Stacks_Bags
{
	public class StaffRecords
	{
		private ConcurrentDictionary<SalesPerson, int> _commissions 
			= new ConcurrentDictionary<SalesPerson, int>();

		public void LogTrade(Trade trade)
		{
			Thread.Sleep(30);
			if (trade.IsSale)
			{
				int tradeValue = trade.Shirt.PricePence * trade.Quantity;
				int commission = tradeValue / 100;
				_commissions.AddOrUpdate(
					trade.Person, commission, (key, oldValue) => oldValue + commission);
			}
		}
		public void DisplayCommissions(IEnumerable<SalesPerson> people)
		{
			Console.WriteLine();
			Console.WriteLine("Bonus by salesperson:");
			foreach (SalesPerson person in people)
			{
				int bonus = _commissions.GetOrAdd(person, 0);//GETORADD works even if the requested item isn't in the dictionary
				Console.WriteLine(
					$"{person.Name,15} earned ${bonus / 100}.{bonus % 100:00}");
			}
		}
	}
}
