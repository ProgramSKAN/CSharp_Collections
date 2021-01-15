using Collections.Models;
using Collections.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
/*
----------List<T>---------------
 *Similar to Arrays, but can be resized.(Arrays are fixed size)
 *useful when how many items unkown at instantiation.
 *Arrays can't be instantiated without the size.Arrays are not good choice when reeding external data as you donno size.use List.
 *List is flexible than Array
 *every collection tells how many elements are in the collection.Almost all collections have property COUNT, execpt for array LENGTH
 *INSERT * REMOVE from list casuses to shift all the data after index which reduces performance.careful
 *FOREACH is only for reading a collection.(strictly readonly).for modification use forloop
 */ 
namespace Collections
{
    public class Coll_Lists
    {
        public static void Run()
        {
			List<int> ints = new List<int>();//List<T> always starts with empty.it don't have a special constructor.
											 //int[] ints = new int[10];//Arrays instantiated with special sytax [], as it part of .net Runtime
											 //Lists are not part of .Net Runtime.so,normal syntax for new objects
			List<string> daysOfWeek = new List<string>
			{
				"Monday",
				"Tuesday",
				"Wednesday",
				"Thursday",
				"Friday",
				"Saturday",
				"Sunday"
			};//unlike array,here Under the hood the compilar turns collection initalizer{} to .Add("day") method like below.

			List<string> daysOfWeek1 = new List<string>();
			daysOfWeek1.Add("Monday");
			daysOfWeek1.Add("Tuesday");
			daysOfWeek1.Add("Wednesday");
			daysOfWeek1.Add("Thursday");

			//---------------List<T> ---------
			//T is a generic type.Generic type applies to all the collections in C# except Arrays. 
			//Arrays can't be generic.
			//T[] generic=new T[10];//invalid


			//--------------------------
			string filePath = @".\Resources\Pop by Largest Final.csv";
			CsvReaderWithList reader = new CsvReaderWithList(filePath);

			List<Country> countries = reader.ReadAllCountries();

			// This is the code that inserts and then subsequently removes Lilliput.
			// Comment out the RemoveAt to see the list with Lilliput in it.
			Country lilliput = new Country("Lilliput", "LIL", "Somewhere", 2_000_000);
			int lilliputIndex = countries.FindIndex(x => x.Population < 2_000_000);

			countries.Insert(lilliputIndex, lilliput);
            Console.WriteLine("-----------"+lilliputIndex+"---"+lilliput.Name+"------------");
			countries.RemoveAt(lilliputIndex);
			//INSERT * REMOVE from list casuses to shift all the data after index which reduces performance.careful
			
			foreach (Country country in countries)
			{
				Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}
			Console.WriteLine($"{countries.Count} countries");

			//finding item by code
			int index = countries.FindIndex(x => x.Code == "USA");
			Country selectedCountry = countries[index];
			Console.WriteLine("---------SelectedCountry: "+JsonConvert.SerializeObject(selectedCountry));
			//OR
			//USE DICTIONARY //prefer




			//---------------MANIPULATING LIST DATA-------------------------
			//foreach is simple but it has no control.
			//for loop give finer control for LISTS AND ARRAYS.difficult for DICTIONARIES.


			string filePath1 = @".\Resources\Pop by Largest Final.csv";
			CsvReaderWithList reader1 = new CsvReaderWithList(filePath1);

			List<Country> countries1 = reader.ReadAllCountries();

			// comment this out to see all countries, without removing the ones with commas
			reader1.RemoveCommaCountries(countries1);

			Console.Write("Enter no. of countries to display> ");
			bool inputIsInt = int.TryParse(Console.ReadLine(), out int userInput);
			if (!inputIsInt || userInput <= 0)
			{
				Console.WriteLine("You must type in a +ve integer. Exiting");
				return;
			}

			//int maxToDisplay = Math.Min(userInput, countries.Count);
			int maxToDisplay = userInput;

			//display from start
			for (int i = 0; i < countries.Count; i++)
			{
				if (i > 0 && (i % maxToDisplay == 0))
				{
					Console.WriteLine("Hit return to continue, anything else to quit>");
					if (Console.ReadLine() != "")
						break;
				}

				Country country = countries[i];
				Console.WriteLine($"{i + 1}: {PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}

			//display from end
			for (int i = countries.Count - 1; i >= 0; i--)
			{
				int displayIndex = countries.Count - 1 - i;
				if (displayIndex > 0 && (displayIndex % maxToDisplay == 0))
				{
					Console.WriteLine("Hit return to continue, anything else to quit>");
					if (Console.ReadLine() != "")
						break;
				}

				Country country = countries[i];
				Console.WriteLine($"{displayIndex + 1}: {PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}
		}
    }
}
