using System;
using System.Collections.Generic;
using System.Text;

namespace Collections._2.Concurrent_Dictionary
{
    //-------ConcurrentDictionary-----------
    //replacing Dictionary to ConcurrentDictionary is not sufficient.Logic also must be adjusted because it don't know what other threads are doing.
    //often use TryXXX() methods instead of regular methods
    public class Coll_CoDictionary
    {
        public static void Run()
        {
            //StockController controller = new StockController(TShirtProvider.AllShirts);
            //TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);//500ms
            //new SalesPerson("Jack").Work(workDay, controller);
            //new SalesPerson("Anna").Work(workDay, controller);
            //new SalesPerson("Mike").Work(workDay, controller);
            //controller.DisplayStock();

            //CONCURRENT
            StockController_WithConcurrent controller = new StockController_WithConcurrent(TShirtProvider.AllShirts);
            TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);//500ms
            new SalesPerson_WithConcurrent("Jack").Work(workDay, controller);
            new SalesPerson_WithConcurrent("Anna").Work(workDay, controller);
            new SalesPerson_WithConcurrent("Mike").Work(workDay, controller);
            controller.DisplayStock();
            //Use TryGetValue(),TryRemove() in CONCURRENT otherwise this makes same shits sold by multiple salespersons.

            //In Any Concurrent Colections:
            //The Collection might not contain the values you are expecting even if you only checked that collection a moment ago.It could still change If other threads are accessing.modifying them.
            //Always use TryXXX() methods which don't presume knowledge of state
        }
    }
}
