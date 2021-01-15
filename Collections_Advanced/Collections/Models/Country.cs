using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Models
{
    public class Country
    {
		public string Name { get; }
		public string Code { get; }
		public string Region { get; }
		public int Population { get; }
		public Country(string name, string code, string region, int population)
		{
			this.Name = name;
			this.Code = code;
			this.Region = region;
			this.Population = population;
		}
		public override string ToString() => $"{Name} ({Code})";
	}


	//with countrycode custom type
	public class Country1
	{
		public string Name { get; }
		public CountryCode Code { get; }
		public string Region { get; }
		public int Population { get; }
		public Country1(string name, string code, string region, int population)
		{
			this.Name = name;
			this.Code = new CountryCode(code);
			this.Region = region;
			this.Population = population;
		}
		public override string ToString() => $"{Name} ({Code})";
	}
}
