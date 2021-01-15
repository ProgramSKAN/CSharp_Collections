using Collections.Stack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Queue
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
