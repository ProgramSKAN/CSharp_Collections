using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.HashSet_and_SortedSet
{
	public class Customer
	{
		public string Name { get; }
		public List<Tour> BookedTours { get; } = new List<Tour>();
		public Customer(string name)
		{
			this.Name = name;
		}
		public override string ToString() => Name;
	}
}
