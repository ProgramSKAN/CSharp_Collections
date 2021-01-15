using Collections.Models;
using Collections.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collections
{
    public class Collection_Performance
    {
		public static List<Country> AllCountries { get; private set; }
		public static void Run()
        {
			List<DateTime> bankHols1 = new List<DateTime>
			{
				new DateTime(2021, 1, 1),
				new DateTime(2021, 4, 2),
				new DateTime(2021, 4, 5),
				new DateTime(2021, 5, 3),
				new DateTime(2021, 5, 31),
				new DateTime(2021, 8, 30),
				new DateTime(2021, 12, 27),
				new DateTime(2021, 12, 28),
			};
			bankHols1.RemoveAt(0);//this remove first element and shift all elements left.  
								  //O(N) time //bad for large collection data

			var item = bankHols1[3];//get>   O(1)
			bankHols1[3] = new DateTime(2021, 5, 3);//set>  O(1)

		   //for (int i = bankHols1.Count-1; i >=0; i++) //O(N^2)  //less performance
		   //{
			//	if (bankHols1[i] > DateTime.MinValue)
			//		bankHols1.RemoveAt(i);//O(N) for N times
			//}

			bankHols1.RemoveAll(x => x > DateTime.MinValue);//O(N)  //prefer

			/*O(1)
			 * O(logN)
			 * ------
			 * O(N)
			 * O(NlogN)
			 * ------
			 * O(N^2)  //worst  //never keep O(N) inside loop
			 */


			//--------------------------------
			CsvReader reader = new CsvReader(@".\Resources\PopByLargest.csv");
			AllCountries = reader.ReadAllCountries();//copies memory address only
			//AllCountries.Sort();//slightly faster than link
			AllCountries = AllCountries.OrderBy(x=>x.Name).ToList();//.ToList()  >means making new collection.not just copying reference  //O()NlogN) orderby
			foreach (var country in AllCountries)
            {
                Console.WriteLine(country.ToString());
            }

            Console.WriteLine("-----------------Find a county------------");
			Country country1= AllCountries.Find(x => x.Code == "USA");//LINEAR SEARCH  //O(N)
            Console.WriteLine(country1);
		}
	}
}
