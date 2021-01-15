using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections._6.BestPrctices_ConcurrentCollections
{
	public static class Worker
	{
		public static int DoSomething()
		{
			int total = 0;
			for (int i = 0; i < 1000; i++)
				total += i;
			return total;
		}
	}
}
