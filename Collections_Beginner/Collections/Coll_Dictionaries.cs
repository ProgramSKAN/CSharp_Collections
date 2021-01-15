using Collections.Models;
using Collections.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*-----------DICTIONARIES<TKey,TValue>-------------
 * Key-Value-Pair 
 * look up items with a key
 * KEYS should always be unique.(duplicate values can be added in Arrays and Lists)
 * great for unordered data 
 * in LIST/ARRAYS, items are stored in strict order and lookup items using index.
 * in Disctionary items stored in random order with key.
 * T >Generic type
 * All collections apart from arrays are Generic types, because any collection has to be collection of something.
 */

namespace Collections
{
    public class Coll_Dictionaries
    {
        public static void Run()
        {
			Country norway = new Country("Norway", "NOR", "Europe", 5_282_223);
			Country finland = new Country("Finland", "FIN", "Europe", 5_511_303);
			Country updatedFinland = new Country("Updated Finland", "FIN", "Europe", 5_511_303);

			var countries = new Dictionary<string, Country>();//like List, Dictionary also starts its life empty

			countries.Add(norway.Code, norway);
			//countries.Add(norway.Code, norway);//invalid, KEYS should always be unique
			countries.Add(finland.Code, finland);

			//There is no INSERT for dictionary because i	t is not ordered.So, putting a value in particular location is meaningless.

			Console.WriteLine("Enumerating...");
			foreach (var contry in countries)
				Console.WriteLine(contry.Value.Name);
			//or
			foreach (Country nextCountry in countries.Values)
				Console.WriteLine(nextCountry.Name);

			foreach (string key in countries.Keys)
				Console.WriteLine(key);
			Console.WriteLine();

			//Console.WriteLine(countries["MUS"].Name); //error: Keynotfound exception
			bool exists = countries.TryGetValue("MUS", out Country country);
			if (exists)
				Console.WriteLine(country.Name);
			else
				Console.WriteLine("There is no country with the code MUS");


            Console.WriteLine("-------------------------------------------");
			//COLLECTION INITIALIZER for Disctionary
			var countries1 = new Dictionary<string, Country>()
			{
				{norway.Code, norway },
				{finland.Code, finland }
			};//complier uses Add().Dictionaries always starts its life empty
			countries1.Remove(norway.Code);
			countries1.ContainsKey(norway.Code);

			countries1.Select(i => $"{i.Key}: {i.Value.Name}").ToList().ForEach(Console.WriteLine);
			countries1[finland.Code] = updatedFinland;
			countries1.Select(i => $"{i.Key}: {i.Value.Name}").ToList().ForEach(Console.WriteLine);


			//--------------------------------------------------
			string filePath = @".\Resources\Pop by Largest Final.csv";
			CsvReaderWithDictionary reader = new CsvReaderWithDictionary(filePath);
			Dictionary<string, Country> countries2 = reader.ReadAllCountries();

			// NB. Bear in mind when trying this code that string comparison is by default case-sensitive.
			// Hence, for example, to display Finland, you need to type in "FIN" in capitals. "fin" won't work.
			Console.WriteLine("Which country code do you want to look up? ");
			string userInput = Console.ReadLine();

			bool gotCountry = countries2.TryGetValue(userInput, out Country country1);
			if (!gotCountry)
				Console.WriteLine($"Sorry, there is no country with code, {userInput}");
			else
				Console.WriteLine($"{country1.Name} has population {PopulationFormatter.FormatPopulation(country1.Population)}");

		}
	}
}
