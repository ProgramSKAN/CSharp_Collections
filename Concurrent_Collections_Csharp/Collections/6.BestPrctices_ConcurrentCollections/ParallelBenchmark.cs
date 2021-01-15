using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Collections._6.BestPrctices_ConcurrentCollections
{
	//Parallel Type offers Multi-threaded version of common operations like Parallel.For(),Parallel.ForEach()
	//Parallel.For(),Parallel.ForEach() do same as for(),foreach() but they try to distribute the iterations of loop body as efficiently as they can between multiple threads.
	class ParallelBenchmark
	{
		static void Populate(ConcurrentDictionary<int, int> dict, int dictSize)
		{
			Parallel.For(0, dictSize, (i) =>
				{
					dict.TryAdd(i, 1);
					Worker.DoSomething();// with this , in this loop where each time you access ConcurrentDictionary, you then do something significant that only uses local data.in this case only there will advantage in using multiple threads.
				});
		}
		static int Enumerate(ConcurrentDictionary<int, int> dict)
		{
			int expectedTotal = dict.Count;

			int total = 0;
			Parallel.ForEach(dict, keyValPair =>
				 {
					 Interlocked.Add(ref total, keyValPair.Value);//Interlocked.Add  >(+=) to total up the values in the dictonary.they are thread-safe, Avoids Race conditions when adding total
					 Worker.DoSomething();//*
				 });
			return total;
		}
		static int Lookup(ConcurrentDictionary<int, int> dict)
		{
			int total = 0;
			Parallel.For(0, dict.Count, (int i) =>
				{
					Interlocked.Add(ref total, dict[i]);
					Worker.DoSomething();//*
				});
			return total;
		}

		static int Lookup1(ConcurrentDictionary<int, int> dict)
		{
			int total = 0;
			Parallel.For(0, dict.Count, (int i) =>
			{
				int count = dict.Count;//*simple cache of value
				Interlocked.Add(ref total, dict[i]);
				Worker.DoSomething();//*
			});
			return total;
		}
		public static void Benchmark(ConcurrentDictionary<int, int> dict, int dictSize)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();
			Populate(dict, dictSize);
			stopwatch.Stop();
			Console.WriteLine($"Build:     {stopwatch.ElapsedMilliseconds} ms");

			stopwatch.Restart();
			int total = Enumerate(dict);
			stopwatch.Stop();
			Console.WriteLine($"Enumerate: {stopwatch.ElapsedMilliseconds} ms");
			if (total != dictSize)
				Console.WriteLine($"ERROR: Total was {total}, expected {dictSize}");

			stopwatch.Restart();
			int total2 = Lookup(dict);
			stopwatch.Stop();
			Console.WriteLine($"Lookup:    {stopwatch.ElapsedMilliseconds} ms");
			if (total != dictSize)
				Console.WriteLine($"ERROR: Total was {total}, expected {dictSize}");

			stopwatch.Restart();
			int total3 = Lookup1(dict);
			stopwatch.Stop();
			Console.WriteLine($"Lookup1:    {stopwatch.ElapsedMilliseconds} ms");
			if (total != dictSize)
				Console.WriteLine($"ERROR: Total was {total}, expected {dictSize}");
		}
	}
}
