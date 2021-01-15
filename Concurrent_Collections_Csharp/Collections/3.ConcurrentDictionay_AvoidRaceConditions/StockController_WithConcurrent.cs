using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Collections.ObjectModel;

namespace Collections._3.ConcurrentDictionay_AvoidRaceConditions
{
	public class StockController_WithConcurrent
	{
		private ConcurrentDictionary<string, int> _stock = new ConcurrentDictionary<string, int>();
		int _totalQuantityBought;
		int _totalQuantitySold;
		public void BuyShirts(string code, int quantityToBuy)
		{
			/*
			if (!_stock.ContainsKey(code))
				_stock.TryAdd(code, 0);  //this code is fine

			//_stock[code] += quantityToBuy; //this line is not atomic.the other thread could change value before assigning.
			int currentStock = _stock[code];
			int newStock = currentStock + quantityToBuy;
			bool success = _stock.TryUpdate(code, newStock, currentStock);//if existing value is "currentStock" then only update to "newStock"
			//this also won't work, because currentStock value might change before above line runs.//RACE CONDITION

			_totalQuantityBought += quantityToBuy;//not atomic
			*/

			//TO AVOID RACE CONDITIONS
			_stock.AddOrUpdate(code, quantityToBuy, (key, oldValue) => oldValue + quantityToBuy);//*1 //its updates the dictionary, but adds the value if necessary
			Interlocked.Add(ref _totalQuantityBought, quantityToBuy);//*2 //same as +=
																	 //Interlocked provides thread-safe versions of some simple operations
																	 //in above the correct solution requires just one call for concurrentdictionary method ADDORUPDATE().incorrect solution involved multiple calls to concurrentDictionary methods.that's important because
																	 //Whenever you have multiple calls, there is a chance for other threads to modify the collection in between those method calls, and if those calls form part of the same operation, the you Risk Data corruption from RACE CONDTIONS.
																	 //i.e,CONCURRENT COLLECTIONS gives thread-safety, but only while each concurrent collection method is executing.not between method calls.

			//GOOD PRACTICE GUIDELINES:
			//1)To Avoid Race Conditions,Try to make sure each operation on the collection can be done in single call to one of the concurrent collection methods such as ADDORUPDATE.
			//ie.,Aim for one single concurrent collection method call per operation

			//in above code between *1,*2 also it is possible another thread can sneak in and modify.But thats fine because ADDORUPDATE & INTERLOCKED.ADD are 2 independent operations acting on 2 different pieces of data.
			//So, Although inprinciple there is posibility of Race condition, but no Data corruption there.

			/*OR------WITHOUT USING AddOrUpdate()----------------
				_stock.TryAdd(code, 0);

				// now keep trying to update the value. If the update fails, we continue trying until it succeeds.
				// It's only going to fail if another thread sneaks in and changes the stock for this shirt
				// in between the getting the stock and updating it, and we'd have to be incredibly unlucky for 
				// that to happen more than once or twice in succession, so in any normal situation, this loop
				// isn't going to iterate more than a couple of times.

				// Note that the indexer is fine here because we made sure the item is in the 
				// dictionary AND there is no code anywhere in StockController that can remove it from 
				// the dictionary, once added.
				bool success;
				do
				{
					int oldStock = _stock[code];
					success = _stock.TryUpdate(code, oldStock + quantity, oldStock);
				} while (!success);

				Interlocked.Add(ref _totalQuantityBought, quantity);
			 */
		}

		public bool TrySellShirt(string code)
		{
			/*
			if (_stock.TryGetValue(code, out int stock) && stock > 0)
			{
				//	NOT ATOMIC
				//	--_stock[code];  
				//	++_totalQuantitySold;
				//	return true;  
				//OR

				//THIS WON'T WORK because Multiple method calls (TRYGETVALUE,ADDORUPDATE) implies risk of race condition.Ex:TRYGETVALUE say there is 1 shirt, but by the time code reaches ADDORUPDATE that shirt could already be sold but another thread.>(DATA CORRUPTION).
					//_stock.AddOrUpdate(code, 0, (key, oldValue) => oldValue - 1);
					//Interlocked.Increment(ref _totalQuantitySold);
					//return true;
				
			}
			else
				return false;
			*/

			//YOU NEED ENTIRE OPERATION IN SINGLE METHOD CALL

			//_stock.AddOrUpdate(code, 0, (key, oldValue) => oldValue > 0 ? oldValue - 1 : oldValue);
			//now update _totalQuantitySold.but how to know whether shirt is sold or not?
			//Solution is

			bool success = false;
			int newStockLevel = _stock.AddOrUpdate(code,
				(itemName) => { success = false; return 0; },
				(itemName, oldValue) =>
				{
					if (oldValue == 0)
					{
						success = false;
						return 0;
					}
					else
					{
						success = true;
						return oldValue - 1;
					}
				});
			if (success)
				Interlocked.Increment(ref _totalQuantitySold);
			return success;
			/*2nd parameter of AddOrUpdate has success = false; again why? 
			the reason for explicitly setting the closure variable to false is because
			There's a theoretical possibility that the lambda you supply here might be 
			executed multiple times. The reason is that unlike other ConcurrentDictionary 
			methods, AddOrUpdate isn't entirely atomic, and it's possible that the 
			ConcurrentDictionary might calculate a new value, but then discover it can't use 
			the results because another thread has modified the item, so it has to run the 
			delegate again. That's a problem for us because our update lambda has a side effect.
			It's modifies the closure variable to be true. So to play absolutely safe,you want to 
			make sure that whenever the ConcurrentDictionary tries to evaluate a new value, 
			it also overwrites any side effects from whatever previous calculations it's done, 
			hence we changed the second AddOrUpdate parameter to be a lambda.
			here obviously is that you need to make sure that the delegates you supply to AddOrUpdate 
			can execute multiple times without causing bugs.
			*/
		}

		public void DisplayStock()
		{
			Console.WriteLine("Stock levels by item:");
			foreach (TShirt shirt in TShirtProvider.AllShirts)
			{
				//_stock.TryGetValue(shirt.Code, out int stockLevel);
				int stockLevel = _stock.GetOrAdd(shirt.Code, 0); //GETORADD ensures item is in the dictionary,TRYGETVALUE doesn't.in this case both works.
				Console.WriteLine($"{shirt.Name,-30}: {stockLevel}");
			}

			int totalStock = _stock.Values.Sum();//==_totalQuantityBought-_totalQuantitySold
			Console.WriteLine($"\r\nBought = {_totalQuantityBought}");
			Console.WriteLine($"Sold   = {_totalQuantitySold}");
			Console.WriteLine($"Stock  = {totalStock}");
			int error = totalStock + _totalQuantitySold - _totalQuantityBought;
			if (error == 0)
				Console.WriteLine("Stock levels match");//if error=0, then dictionary has no thread sync issues.
			else
				Console.WriteLine($"Error in stock level: {error}");
		}
	}
}
