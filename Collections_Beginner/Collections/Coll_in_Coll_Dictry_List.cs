using Collections.Models;
using Collections.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collections
{
    public class Coll_in_Coll_Dictry_List
    {
		public static void Run()
        {
			string filePath = @".\Resources\Pop by Largest Final.csv";
			CsvReaderWithDictryAndList reader = new CsvReaderWithDictryAndList(filePath);

			Dictionary<string, List<Country>> countries = reader.ReadAllCountries();

			foreach (string region in countries.Keys)
				Console.WriteLine(region);

			Console.Write("Which of the above regions do you want? ");
			string chosenRegion = Console.ReadLine();

			if (countries.ContainsKey(chosenRegion))
			{
				// display 10 highest population countries in the selected region
				foreach (Country country in countries[chosenRegion].Take(10))
					Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}
			else
				Console.WriteLine("That is not a valid region");
		}
    }
}
