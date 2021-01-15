using Collections._1.Collections_And_Atomic_Operations;
using Collections._2.Concurrent_Dictionary;
using Collections._3.ConcurrentDictionay_AvoidRaceConditions;
using Collections._4.ProducerConsumerCollections_Concurrent_Queues_Stacks_Bags;
using Collections._5.BlockingCollection_and_ConsumingEnumerable;
using Collections._6.BestPrctices_ConcurrentCollections;
using Collections._7.Enumerating_ConcurrentCollections;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            //Coll_CoQueue.Run();
            //Coll_CoDictionary.Run();

            //In Any Concurrent Colections:
            //The Collection might not contain the values you are expecting even if you only checked that collection a moment ago.It could still change If other threads are accessing.modifying them.
            //Always use TryXXX() ATOMIC methods which don't presume knowledge of state
            //Ex:   Dictionary         |            ConcurrentDictionary (Atomic methods)
            //      Remove()           |            TryRemove()
            //      TryGetValue()      |            TryGetValue()

            //there are 2 problems in 2.Concurrent_Dictionary, after making concurrent
            //1)Data Corruption
            //2)Race Condition

            //Techniques to Avoid Race Conditions
            //1)AddOrUpdate()  and   GetOrAdd()
            //2)try to Complete operations in single concurrent collection method call whenever possible


            //Race Condition Demo project  :  Buy(In batches of 1-9 shirts) and sell(one at a time) shirts at same time
            //must keep track of stock levels
            //start each day with zero stock
            //Now ConcurrentDictionary must store actual stock levels for each shirt, not merely say which shirts are in stock.

            //CoDic_AvoidRace.Run();



            //--------------------------------
            //Coll_CoQu_CoSt_CoBg.Run();
            //Coll_BlockingColl.Run();


            //----------Performance BenchMark--------------
            //Dtnry_CoDtrny_BenchMark.Run();

            //----------Collection Enumerating--------------
            Coll_Enumerating.Run();


        }
    }
}
