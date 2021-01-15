using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-------------IMMUTABLE COLLECTIONS-----------------
//collections in their own right
//countris.ToImmutableArray() makes
//--copies items in countries to a brand new collection
//--the immutable collection dont see changes to contries


//System.Collections.Immutable;
//most of the collections has its immutable counterpart


namespace Collections.HashSet_and_SortedSet
{
	public class Coll_immutable_Collections
	{
		//public List<Country> AllCountries { get; private set; }
		//public Dictionary<CountryCode, Country> AllCountriesByKey { get; private set; }
		public ImmutableArray<Country> AllCountries { get; private set; }//IReadOnlyList<Country>
		public ImmutableDictionary<CountryCode, Country> AllCountriesByKey { get; private set; }



		public List<Customer> Customers { get; private set; } = new List<Customer>() { new Customer("Simon"), new Customer("Kim") };
		public ConcurrentQueue<(Customer TheCustomer, Tour TheTour)> BookingRequests { get; } = new ConcurrentQueue<(Customer, Tour)>();
		public LinkedList<Country> ItineraryBuilder { get; } = new LinkedList<Country>();
		public SortedDictionary<string, Tour> AllTours { get; private set; } = new SortedDictionary<string, Tour>();
		public Stack<ItineraryChange> ChangeLog { get; } = new Stack<ItineraryChange>();



		public void Run()
		{
			CsvReader reader = new CsvReader(@".\Resources\PopByLargest.csv");
			//this.AllCountries = reader.ReadAllCountries().OrderBy(x=>x.Name).ToList();
			var countris = reader.ReadAllCountries().OrderBy(x => x.Name).ToList();
			this.AllCountries = countris.ToImmutableArray();
			
			this.AllCountriesByKey = AllCountries.ToImmutableDictionary(x => x.Code);

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


			countris.Add(new Country("Lilliput", "LIL", "somewhere", 1_000_000));//this will not be in the list.as its immutable
			//------------------------COUNTRIES---------------------

			foreach (var item in AllCountries)
            {
				Console.WriteLine(string.Join(Environment.NewLine, item));
			}

		}


	}
}
