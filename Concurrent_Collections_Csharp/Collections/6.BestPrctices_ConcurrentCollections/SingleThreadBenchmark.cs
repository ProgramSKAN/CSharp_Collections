using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Collections._6.BestPrctices_ConcurrentCollections
{
	//IDictionary<int, int>  could be Dictionary<Tk,Tv> or ConcurrentDictionary<Tk,Tv>
	class SingleThreadBenchmark
	{
		static void Populate(IDictionary<int, int> dict, int dictSize)
		{
			for (int i = 0; i < dictSize; i++)
			{
				dict.Add(i, 1); //ConcurrentDictionary<Tk,Tv> dont have Add() method.but here it has to use Add() to support IDictionary interface.
								//still you should be careful , because all the issues with Add() in Concurrancy when you donno what other threads are doing still apply.in this ex: Add() always succeeds
				Worker.DoSomething();//*
			}
		}
		static int Enumerate(IDictionary<int, int> dict)
		{
			int total = 0;
			foreach (var keyValPair in dict)
			{
				total += keyValPair.Value;//total=no of elements in Dictionary. as value=1 for all
				Worker.DoSomething();//*
			}
			return total;
		}
		static int Lookup(IDictionary<int, int> dict)
		{
			int total = 0;
			int count = dict.Count;
			for(int i=0; i<count; i++)
			{
				total += dict[i];//this will BenchMark Dictionary Lookup
				Worker.DoSomething();//*
			}
			return total;
		}

		static int Lookup1(IDictionary<int, int> dict)
		{
			int total = 0;
			//int count = dict.Count;  //no impact in logic 
			for (int i = 0; i < dict.Count; i++)
			{
				total += dict[i];
				Worker.DoSomething();
			}
			return total;
		}

		public static void Benchmark(IDictionary<int, int> dict, int dictSize)
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
