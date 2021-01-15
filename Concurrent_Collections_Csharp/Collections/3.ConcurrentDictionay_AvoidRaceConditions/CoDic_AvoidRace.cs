using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collections._3.ConcurrentDictionay_AvoidRaceConditions
{
	public class CoDic_AvoidRace
	{
		public static void Run()
		{
			//StockController controller = new StockController();
			//TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);

			//new SalesPerson("Jack").Work(workDay, controller);
			//new SalesPerson("Anna").Work(workDay, controller);
			//new SalesPerson("Bond").Work(workDay, controller);

			//controller.DisplayStock();


			StockController_WithConcurrent controller = new StockController_WithConcurrent();
			TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);

			Task task1 = Task.Run(() => new SalesPerson_Concurrent("Jack").Work(workDay, controller));
			Task task2 = Task.Run(() => new SalesPerson_Concurrent("Anna").Work(workDay, controller));
			Task task3 = Task.Run(() => new SalesPerson_Concurrent("Bond").Work(workDay, controller));
			Task task4 = Task.Run(() => new SalesPerson_Concurrent("Kim").Work(workDay, controller));
			Task.WaitAll(task1, task2, task3, task4);

			controller.DisplayStock();
			//---------CONCURRENT DICTIONARY METHODS------------
			//       AddOrUpdate()           |            GetOrAdd()
			// Solution for updating         |    solution for reading
			//Guarenteed to succeed          |		Guarenteed to succeed
			//Add item if not already there  |    Adds the item if it's not there
		}
	}
}