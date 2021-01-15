using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-------------READ ONLY COLLECTIONS-----------------
//Just Wrappers that guard other collections
//countris.ReadOnly() makes
//--create thin wrapper around countries
//--the readonly collection sees changes to contries


namespace Collections.HashSet_and_SortedSet
{
	public class Coll_ReadOnly_Collections
	{
		//public List<Country> AllCountries { get; private set; }
		//public Dictionary<CountryCode, Country> AllCountriesByKey { get; private set; }
		public ReadOnlyCollection<Country> AllCountries { get; private set; }
		public ReadOnlyDictionary<CountryCode, Country> AllCountriesByKey { get; private set; }



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
			this.AllCountries = countris.AsReadOnly();
			//LoopHole in ReadOnlyCollection > AsReadOnly is just a wrapper arount List to make ReadOnlyCollection.But contris is still be modifiable.
			//this could be useful in cases where external code can access ReadOnlyCollection,but internal code can still modfiy
			//if you want the collection to be completely fixed(not modification once instantiated) then use "IMMUTABLE COLLECTIONS"

			var dict = AllCountries.ToDictionary(x => x.Code);//there is not readonly conversion method for dictionary
			//this.AllCountriesByKey = dict;
			this.AllCountriesByKey = new ReadOnlyDictionary<CountryCode, Country>(dict);

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



			countris.Add(new Country("Lilliput", "LIL", "somewhere", 1_000_000));//this will be in the list as it added underlying list inside readonly wrapper
                                                                                 //------------------------COUNTRIES---------------------

            foreach (var item in AllCountries)
            {
				Console.WriteLine(string.Join(Environment.NewLine, item));
			}

		}


	}
}
