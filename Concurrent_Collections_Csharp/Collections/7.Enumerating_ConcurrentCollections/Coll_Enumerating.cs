using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Collections._7.Enumerating_ConcurrentCollections
{
    //What happens if collections modifed while Enumerating?
    //solution:
    //carry on regardless
    //Take a snapshot

    /*Enumerating Concurrent Collections looks same way as Standard Collections
     *
         foreach(TShirt shirt in _stock.Value)
            Console.WriteLine(shirt);
        //OR LINQ
        var shirts=from TShirt in _stock.Values select shirt;
     
        However Concurrent Collection enumerators don't always behave same way as enumerators of Standard generic Collections
     */

    public class Coll_Enumerating
    {
        public static void Run()
        {
            Console.WriteLine("------------------STANDARD DICTIONARY---------------------");
            var stock = new Dictionary<string, int>();
            stock.Add("BigData", 0);
            stock.Add("Machine Learning", 0);
            stock.Add("docker", 0);

            foreach (var shirt in stock)
            {
                Console.WriteLine(shirt.Key + ": " + shirt.Value);
            }
            //Big Data: 0
            //Machine Learning: 0
            //docker: 0

            /*foreach (var shirt in stock)
            {
                stock["BigData"] += 1;//attempting to modify the collection while enumeratin it
                //Exception: Collection was modified; enumeration operation may not execute.
                Console.WriteLine(shirt.Key + ": " + shirt.Value);
            }*/

            //In Standard Collection , it refuses to enumerate if collection modifies while enumerating.

            //--------------------------------
            Console.WriteLine("------------------CONCURRENT DICTIONARY---------------------");
            //In concurrent environment, it is impossible to throw exception if collection modifies while enumerating.
            //because enumerator can't stop other threads modifying the collection.
            //SOLUTION 1:Take snapshot of the collection.and fire the enumerate to enumerate the snapshot.
            //SOLUTION 2: deal with any modifications as they happen.

            //ConcurrentDictionary offers both depending on how you enumerate it.
            var stock1 = new ConcurrentDictionary<string, int>();
            stock1.TryAdd("BigData", 0);
            stock1.TryAdd("Machine Learning", 0);
            stock1.TryAdd("docker", 0);

            foreach (var shirt in stock1)
            {
                stock1["BigData"] += 1;//it is fine here because we are sure that no other threads running at a point when this line executes.
                                        //it there are other threads, use AddOrUpdate()
                Console.WriteLine(shirt.Key + ": " + shirt.Value);
            }//result chages for every run
            //docker: 0
            //Machine Learning: 0
            //BigData: 2


            //here, the enumeration order has changed.it is unrelated to the order that the items are added.
            //so , don't rely on dictionary enumeration order.

            //BigData = 2 instead of 3
            //so, enumerating "ConcurrentDictionary" while modifing will enumerate, but no guarantee of the results.
            //if u add/remove elements, there is no guarantee that enumeration sees those elements.
            //IN CONCURRENT DICTIONARY: Enumeration results are unpredictable 

            //--------------------------------
            Console.WriteLine("---------TO GET PREDICTABLE ENUMERATION, Take a snapshot of the collection----------");
            var stock2 = new ConcurrentDictionary<string, int>();
            stock2.TryAdd("BigData", 0);
            stock2.TryAdd("Machine Learning", 0);
            stock2.TryAdd("docker", 0);
            foreach (var shirt in stock2.ToArray())
            {
                stock2["BigData"] += 1;
                Console.WriteLine(shirt.Key + ": " + shirt.Value);
            }//it will enumerate at a state of a dictionary as it was when the code first hits the foreach statement.so, enumerate didn't see changes to "BigData"
             //docker: 0
             //BigData: 0
             //Machine Learning: 0

            //ConcurrentDictionary.Keys, ConcurrentDictionary.Values also takes SNAPSHOTS

            //--------------------------------
            //SNAPSHOT:
            //PROS : Guaranteed to represent a consistent state
            //CONS : Have to take a copy of the collection.this can be slow since it requires getting aggregate state of entire collection.so,don't take snapshot too often.

            //JUST ENUMERATING:
            //PROS: More Efficient.
            //CONS: results are not predictable.result are inconsistent since they are not showing the state of the collection at any instant of time.    

            //------------ENUMERATING CONCURRENT COLLECTIONS-----------------------
            //--------GetEnumerate() / foreach----
            //JUST ENUMERATE: ConcurrentDictionary (unless invoking Keys,Values,...  because they are snapshots)
            //SNAPSHOT: ConcurrentDictionary(with invoking Keys,Values,ToArray(),..), ConcurrentStack,ConcurrentQueue,ConcurrentBag.  >this coolections always take snapshot when you enumerate them


            //PREFER Immutable Collections for fixed Data
        }
    }
}
