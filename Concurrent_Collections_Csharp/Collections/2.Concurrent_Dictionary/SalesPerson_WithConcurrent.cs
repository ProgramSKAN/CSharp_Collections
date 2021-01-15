using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Collections._2.Concurrent_Dictionary
{
    public class SalesPerson_WithConcurrent
	{
        public string Name { get; }
        public SalesPerson_WithConcurrent(string name)
        {
            this.Name = name;
        }
		public void Work(TimeSpan workDay, StockController_WithConcurrent controller)
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
		public (bool ShirtsInStock, string Status) ServeCustomer(StockController_WithConcurrent controller)
		{
			//TShirt shirt = controller.SelectRandomShirt();
			(SelectResult Result, TShirt shirt) result = controller.SelectRandomShirt();
			TShirt shirt = result.shirt;

			//if (shirt == null)
			if (result.Result == SelectResult.NoStockLeft)
				return (false, "All shirts sold");
			else if (result.Result == SelectResult.ChosenShirtSold)
				return (true, "Can't show shirt to customer-already sold");

			Thread.Sleep(Rnd.NextInt(30));

			// customer chooses to buy with only 20% probability
			if (Rnd.TrueWithProb(0.2))
			{
				//controller.Sell(shirt.Code);
				//return (true, $"Sold {shirt.Name}");

				bool sold =controller.Sell(shirt.Code);//here, you assume that you can sell the shirt, because a few millisecs ago you looked in _stock concurrantDictionary.But by the time code reaches this line, another thread might have sold it.
													   //so, use TryRemove() inside sell.
				if (sold)
					return (true, $"Sold {shirt.Name}");
				else
					return (true, $"Can't sell {shirt.Name}: Already Sold");//sold by another thread
			}
			return (true, null);
		}
	}
}
