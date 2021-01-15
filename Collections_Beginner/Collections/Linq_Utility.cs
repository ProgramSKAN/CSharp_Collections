using Collections.Models;
using Collections.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collections
{
	//------------LINQ----------Language Integrated Query------------------
	//To query/extracting the dataSources you want
	//LINQ is read-only.not for modification
	//LINQ query syntax

	//FOR LOOP is only for ordered collections(ARRAY,LIST).LINQ works with DICTIONARY too
	public class Linq_Utility
    {
        public static void Run()
        {
			string filePath = @".\Resources\Pop by Largest Final.csv";
			CsvReaderWithList reader = new CsvReaderWithList(filePath);

			List<Country> countries = reader.ReadAllCountries();

			foreach (Country country in countries.OrderBy(x => x.Name))
			{
				Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}
			//intead of printing all values at once.batching countries up would be nice
			//but, LINQ doesn't support batching. use forloop for batching
            Console.WriteLine("------------------------------------------------------------------");

			// listing the first 20 countries without commas in their names
			var filteredCountries = countries.Where(x => !x.Name.Contains(',')).Take(20);
			//OR
			var filteredCountries2 = from country in countries
									 where !country.Name.Contains(",")
									 select country;
			//unlike LINQ method syntax, query syntax don't suppoert all LINQ features(ex: Take(20))
			foreach (Country country in filteredCountries)
			{
				Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}
			//WHERE >it only filters data but it won't remove it physically at an index.LINQ is read-only
			Console.WriteLine();
			Console.WriteLine("------------------------------------------------------------------");

			// listing the 10 highest population countries in alphabetical order.
			// Reverse Where() and Take() to see the impact of swapping chaining order round
			foreach (Country country in countries.Take(10).OrderBy(x => x.Name))
			{
				Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}
			//here think 'countries' as datasorce instead of collection.
			//to LINQ, contries collection is just something that can be enumerated.
			//Most collections implement IEnumerable<T>
			//IEnumerable<T> exposes the ability to supply objects on demand.LINQ methods can leverage that interface.
			//for LINQ, 'countries' is just datasource.it wont care about it is collectio. or not.

			//how it can use orderby without knowing List<T> is a collection?
			//Take is from Enumerable class inside system.Linq Namespace.so, it works with any objects that implement 'IEnumerable<T>'.
			//almost all collections implement IEnumerable<T>, LINQ works for all collections

			//FOR LOOP is for ARRAY & LIST. (not dictionary, as it don't have an index)
			//LINQ works with ARRAY , LIST & DICTIONARY
		}
	}
}
