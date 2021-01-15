using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Collections._2.Concurrent_Dictionary
{
    public class SalesPerson
    {
        public string Name { get; }
        public SalesPerson(string name)
        {
            this.Name = name;
        }
		public void Work(TimeSpan workDay, StockController controller)
		{
			DateTime start = DateTime.Now;
			while (DateTime.Now - start < workDay)
			{
				var result = ServeCustomer(controller);
				if (result.Status != null)
					Console.WriteLine($"{Name}: {result.Status}");
				if (!result.ShirtsInStock)
					break;
			}
		}
		public (bool ShirtsInStock, string Status) ServeCustomer(StockController controller)
		{
			TShirt shirt = controller.SelectRandomShirt();
			if (shirt == null)
				return (false, "All shirts sold");

			Thread.Sleep(Rnd.NextInt(30));

			// customer chooses to buy with only 20% probability
			if (Rnd.TrueWithProb(0.2))
			{
				controller.Sell(shirt.Code);
				return (true, $"Sold {shirt.Name}");
			}
			return (true, null);
		}
	}
}
