using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Collections._3.ConcurrentDictionay_AvoidRaceConditions
{
	public class SalesPerson_Concurrent
	{
		public string Name { get; }
		public SalesPerson_Concurrent(string name)
		{
			this.Name = name;
		}
		public void Work(TimeSpan workDay, StockController_WithConcurrent controller)
		{
			DateTime start = DateTime.Now;
			while (DateTime.Now - start < workDay)
			{
				string msg = ServeCustomer(controller);
				if (msg != null)
					Console.WriteLine($"{Name}: {msg}");
			}
		}
		public string ServeCustomer(StockController_WithConcurrent controller)
		{
			Thread.Sleep(Rnd.NextInt(5));
			TShirt shirt = TShirtProvider.SelectRandomShirt();
			string code = shirt.Code;

			//assuming customer will always buy/sell a shirt
			bool custSells = Rnd.TrueWithProb(1.0/6.0);//customer want to sell a shirt 1/6th of the time
			if (custSells)
			{
				int quantity = Rnd.NextInt(9) + 1;
				controller.BuyShirts(code, quantity);
				return $"Bought {quantity} of {shirt}";
			}
			else
			{
				bool success = controller.TrySellShirt(code);
				if (success)
					return $"Sold {shirt}";
				else
					return $"Couldn't sell {shirt}: Out of stock";
			}
		}
	}
} 
