﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Collections._4.ProducerConsumerCollections_Concurrent_Queues_Stacks_Bags
{
	public class TShirt
	{
		public string Code { get; }
		public string Name { get; }
		public int PricePence { get; }

		public TShirt(string code, string name, int pricePence)
		{
			this.Code = code;
			this.Name = name;
			this.PricePence = pricePence;
		}

		public override string ToString() 
			=> $"{Name} ({DisplayPrice(PricePence)})";

		private string DisplayPrice(int pricePence)
			 => $"${pricePence/100}.{pricePence%100:00}";
	}
}
