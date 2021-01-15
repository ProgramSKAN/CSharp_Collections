using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Collections._6.BestPrctices_ConcurrentCollections
{
	//--------PERFORMANCE-------------
	//Access Concurrent Collections Sparingly
	//Avoid Aggregate State Operations

	//main reson to use Concurrent Collections > to use multiple threads  >for performance/responsiveness
	public class Dtnry_CoDtrny_BenchMark
	{
		public static void Run()
		{
			//since it is "BenchMarking" not debugging, Run The Code in "Release" Build.so the results won't be skewed by app soing extra work to assist debugger.
			int dictSize = 200000;

			Console.WriteLine("Dictionary, single thread:");
			var dict = new Dictionary<int, int>();
			SingleThreadBenchmark.Benchmark(dict, dictSize);

			Console.WriteLine("\r\nConcurrentDictionary, single thread:");
			var dict2 = new ConcurrentDictionary<int, int>();
			SingleThreadBenchmark.Benchmark(dict2, dictSize);

			Console.WriteLine("\r\nConcurrentDictionary, multiple threads:");
			var dict3 = new ConcurrentDictionary<int, int>();
			ParallelBenchmark.Benchmark(dict3, dictSize);


			/*---------Without Worker.DoSomething() in all methods-----------------------
					Dictionary, single thread:
					Build: 17 ms
					Enumerate: 3 ms
					Lookup:    2 ms

					ConcurrentDictionary, single thread:
					Build: 51 ms    //Concurrent Collections do lot of work under the hood to ensure thread-safety.//no benifits of using Concurrent Collections in 1-thread
					Enumerate: 7 ms
					Lookup:    11 ms

					ConcurrentDictionary, multiple threads:
					Build: 79 ms	//much slower than single thread
					Enumerate: 18 ms
					Lookup:    7 ms

					why Build,Enumerate & Lookup in multiple-threads is much slower than 1-thread?
					*this shows how not to use Concurrent Collections
					*Problem : Multiple threads are doing stuff but each thread didn't have a lot to do before it needed to go back the ConcurrentDictionay.(Multiple Threads, each thread not doing much)
					*Ex: in ParallelBenchmark.cs without Worker.DoSomething(), what Populate() is doing that doesn't involve ConcurrentDictinary?nothing
					*	same for Enumerate() and LookUp()
					*	Almost Entire processing time involves shared state(ConcurrentDictinary) in each method.
					*	There are multiple threads but all their time doing stuff with share state only.This would require extra thread synchronization overhead.
					*	so, everything ran slower in multiple-threads Without Worker.DoSomething()
					
			 */
			/*---------With Worker.DoSomething() in all methods-----------------------
			 * Best Scenario to use Multiple Threads
			 
					Dictionary, single thread:
					Build: 68 ms
					Enumerate: 60 ms
					Lookup:    64 ms

					ConcurrentDictionary, single thread:
					Build: 94 ms    
					Enumerate: 61 ms
					Lookup:    66 ms

					ConcurrentDictionary, multiple threads:
					Build: 70 ms	//Better than single thread
					Enumerate: 27 ms
					Lookup:    16 ms
				//FOR CONCURRENT COLLECTIONS PERFORMANCE EBENIFITS, the ensure time spent on accessing the collection is only small part of the work each thread does.That way benefits of concurrancy massively outweighs the loss from thread synchronization overhead.
				//Access the collections Sparingly 
			 */


			/*-------------------WITH LOOKUP1-------------------
			 * //loogingUp count in every iteration of loop make :
			 * 
					Dictionary, single thread:
					Build: 68 ms
					Enumerate: 58 ms
					Lookup:    64 ms
					Lookup1:    59 ms // no change

					ConcurrentDictionary, single thread:
					Build: 91 ms    
					Enumerate: 58 ms
					Lookup:    66 ms
					Lookup1:    3594 ms //very slower performace

					ConcurrentDictionary, multiple threads:
					Build: 65 ms	
					Enumerate: 27 ms
					Lookup:    16 ms
					Lookup:    2766 ms //very slower performace

					Why .Count() is slower in ConcurrentDictionary()?
					* Count() Queries the state of the entire dictionary , not just one element.
					* it like asking for Overall/Aggregate state of Dictionary, because value of count depends on every item in the collection.
					* asking for aggregate state is a problem because concurrent collections are designed to be optimzed for having lot of threads continuously changing data in them.
					*so, it is hard to define the current state.we have synchronize lot of threads for it.
					*Don't Query aggregate state, it really hits performance
					*This applies to ConcurrentDictionary,ConcurrentBag. it is less problematic for ConcurentQueue & ConcurrentStack because of its internal data structure workout.
				
					
					BEST PRACTICE 1:
					*In ConcurrentDictionary()
					*.Count() , .IsEmpty() , .Keys() , .Values() , .ToArray()   ---->all this are slow , because they ask for aggregate state of collection
					*so,Avoid Querying aggregate state of concurrent collections too often.
					
					*BEST PRACTICE 2:
					*in concurrent collections we use TryGetValue(),TryRemove(),TryDequeue(),... instead of dic["key"],dic.Remove() becasue dic["key"],dic.Remove() presume state of the collection.
					*so, Don't rely on the state of a collection (Ex:contains a particular value...) 
					*because info can be out of date(due to other threads)
					*This applies to all CONCURRENT COLLECTIONS
			  */
		}
	}
}
