using Collections.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collections.Queue
{
	//------------------QUEUE-----------------------
	//ENQUEUE/DEQUEUE items. LIFO
    public class Coll_Queue
    {
		public List<Country> AllCountries { get; private set; }
		public Dictionary<CountryCode, Country> AllCountriesByKey { get; private set; }
		public LinkedList<Country> ItineraryBuilder { get; } = new LinkedList<Country>();
		public SortedDictionary<string, Tour> AllTours { get; private set; } = new SortedDictionary<string, Tour>();
		public Stack<ItineraryChange> ChangeLog { get; } = new Stack<ItineraryChange>();



		public List<Customer> Customers { get; private set; } = new List<Customer>() { new Customer("Simon"), new Customer("Kim") };
		public Queue<(Customer TheCustomer, Tour TheTour)> BookingRequests { get; } = new Queue<(Customer, Tour)>();

		public void Run()
        {
			CsvReader reader = new CsvReader(@".\Resources\PopByLargest.csv");
			this.AllCountries = reader.ReadAllCountries().OrderBy(x => x.Name).ToList();
			var dict = AllCountries.ToDictionary(x => x.Code);
			this.AllCountriesByKey = dict;

			//ENQUEUE
			//BookingRequests.Enqueue((new Customer(), new Tour()));

			//DEQUEUE
			if(BookingRequests.Count>0)
				(Customer TheCustomer, Tour TheTour) = BookingRequests.Dequeue();//throws exception if queue is empty
			
			//PEEK
			BookingRequests.Peek().ToString();

            //ENUMERATE
            foreach (var item in BookingRequests)
            {
                Console.WriteLine(item);
            }



		}
    }
}
