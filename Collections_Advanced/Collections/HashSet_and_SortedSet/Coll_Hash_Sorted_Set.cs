using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-------------HASHSET-----------------
//Enforces Uniqueness in vey scalable/efficient way
//unordered bag of unique items

//Dicionary and Hashset are very similar.Both are 'unordered bag of objects','very similar datastructures internally'.
//But Both Purposes are different

//-----Dicionary<TKey,TValue>-------|--------HashSet<T>---------
//Have Keys							|  Don't have Keys
//Key-Based Lookup					|  Don't support lookup(only way to get item in a set is to enumerate them(foreach))
//Keys are Unique					|  Values are Unique 
//Adding Duplicates throws exception|  Duplicates are ignored

//-------------SORTEDSET-----------------
//same as HASHSET , but it automatically sort items as you add them. enumerates in sorted order.
//Both SETS & DICTIONARIES come in sorted & unsorted flavours
//Atleast one object must implement IComparable


namespace Collections.HashSet_and_SortedSet
{
	public class Coll_Hash_Sorted_Set
	{
        public List<Country> AllCountries { get; private set; }
        public Dictionary<CountryCode, Country> AllCountriesByKey { get; private set; }

        public List<Customer> Customers { get; private set; }= new List<Customer>() { new Customer("Simon"), new Customer("Kim") };
		public ConcurrentQueue<(Customer TheCustomer, Tour TheTour)> BookingRequests { get; }= new ConcurrentQueue<(Customer, Tour)>();
		public LinkedList<Country> ItineraryBuilder { get; } = new LinkedList<Country>();
		public SortedDictionary<string, Tour> AllTours { get; private set; } = new SortedDictionary<string, Tour>();
		public Stack<ItineraryChange> ChangeLog { get; } = new Stack<ItineraryChange>();



		public void Run()
		{
			CsvReader reader = new CsvReader(@".\Resources\PopByLargest.csv");
			this.AllCountries = reader.ReadAllCountries().OrderBy(x=>x.Name).ToList();
			
			var dict = AllCountries.ToDictionary(x => x.Code);
			this.AllCountriesByKey = dict;

			//---------add tours--------------
			Country finland = AllCountriesByKey[new CountryCode("FIN")];
			Country greenland = AllCountriesByKey[new CountryCode("GRL")];
			Country iceland = AllCountriesByKey[new CountryCode("ISL")];

			Country newZealand = AllCountriesByKey[new CountryCode("NZL")];
			Country maldives = AllCountriesByKey[new CountryCode("MDV")];
			Country fiji = AllCountriesByKey[new CountryCode("FJI")];

			Country newCaledonia = AllCountriesByKey[new CountryCode("NCL")];

			Tour xmas = new Tour("Snowy Christmas", new Country[] { finland, greenland, iceland });
			AllTours.Add(xmas.Name, xmas);

			Tour islands = new Tour("Exotic Islands", new Country[] { newZealand, maldives, fiji });
			AllTours.Add(islands.Name, islands);

			Tour newTour = new Tour("New Countries", new Country[] { newCaledonia, newZealand, newCaledonia, newZealand });
			AllTours.Add(newTour.Name, newTour);
			//------------------------COUNTRIES---------------------

			var countries = new List<Country>();
			List<Tour> selectedTours = new List<Tour>() { xmas, islands, newTour };
			foreach (var tour in selectedTours)
            {
				foreach (Country country in tour.Itinerary)
					countries.Add(country);
            }
			Console.WriteLine(string.Join(Environment.NewLine,countries));//it has duplicates (New Zealand)

			Console.WriteLine("---------------------countries Without Duplicates using LINQ-------------------");
			var countriesWithoutDuplicates= countries.Distinct().ToList();
			Console.WriteLine(string.Join(Environment.NewLine, countriesWithoutDuplicates));
			//Although LINQ works, its not always efficient/scalable  //because it adds duplicates to list>remove duplicates>then creates list
			//it is fine for small collections.not ideal for large collections.

			Console.WriteLine("---------------------countries Without Duplicates using HASHSET-----efficient--------------");
			var countries1 = new HashSet<Country>();
			List<Tour> selectedTours1 = new List<Tour>() { xmas, islands, newTour };
			foreach (var tour in selectedTours1)
			{
				foreach (Country country in tour.Itinerary)
					countries1.Add(country);
			}
			Console.WriteLine(string.Join(Environment.NewLine, countries1));


			Console.WriteLine("---------------------countries Without Duplicates & with Sorted order using SORTEDSET-------------------");
			var countries2 = new SortedSet<Country>(CountryNameComparer.Instance);
			//sortedset donno how to sort Country instances.so,create Comparer that implements IComparer
			List<Tour> selectedTours2 = new List<Tour>() { xmas, islands, newTour };
			foreach (var tour in selectedTours1)
			{
				foreach (Country country in tour.Itinerary)
					countries2.Add(country);
			}
			Console.WriteLine(string.Join(Environment.NewLine, countries2));

			Console.WriteLine("---------------------countries UNION-------------------");
			List<Tour> selectedTours3 = new List<Tour>() { xmas, islands, newTour };

			var allSets = new List<SortedSet<Country>>();
            foreach (Tour tour in selectedTours3)
            {
				SortedSet<Country> tourCountries = new SortedSet<Country>(tour.Itinerary, CountryNameComparer.Instance);
				allSets.Add(tourCountries);
            }

			SortedSet<Country> allCountriesInTours = allSets[0];
            for (int i = 1; i < allSets.Count; i++)
            {
				allCountriesInTours.UnionWith(allSets[i]);//UnionWith mofifies the result set.it won't create new set.
			}
			//this works with HASHSET also
			Console.WriteLine(string.Join(Environment.NewLine, allCountriesInTours));

			Console.WriteLine("---------------------countries INTERSECTION-------------------");
			SortedSet<Country> commonCountriesInTours = allSets[0];
			for (int i = 1; i < allSets.Count; i++)
			{
				commonCountriesInTours.IntersectWith(allSets[i]);
			}
			Console.WriteLine(string.Join(Environment.NewLine, commonCountriesInTours));
			//SETS are very useful to know which items appears across multiple collections

		}
		

	}
}
