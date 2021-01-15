using Collections.Models;
using Collections.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collections
{
    public class Dictnries_and_SortedDictnries
    {
        public List<Country> AllCountries { get; private set; }
        public Dictionary<string,Country> AllCountriesByKey { get; private set; }

        //SortedDictionary/SortedList only for sorting by key
        public SortedDictionary<string,Country> AllCountriesByKey1 { get; private set; }
        public SortedList<string,Country> AllCountriesByKey2 { get; private set; }

        //CountryCode: coustom type
        public List<Country1> AllCountries1 { get; private set; }
        public Dictionary<CountryCode,Country1> AllCountriesByKey3 { get; private set; }

        public void Run()
        {
            CsvReader reader = new CsvReader(@".\Resources\PopByLargest.csv");
            this.AllCountries = reader.ReadAllCountries().OrderBy(x => x.Name).ToList();
            this.AllCountriesByKey = AllCountries.ToDictionary(x => x.Code);//List to Dictionary

            //SEARCH COUNTRY
            Console.WriteLine("-------------SEARCH COUNTRY (USA)------------------");
            //Country result1 = AllCountries.Find(x => x.Code == "USA");//O(N)
            this.AllCountriesByKey.TryGetValue("USA", out Country result);//O(1) //prefer   //case sensitive
            Console.WriteLine(result);

            //the search is case sensitive
            //Country result1 = AllCountries.Find(x => x.Code.ToUpper() == "usa".ToUpper());
            //like above, for list it is possible to use to upper
            //for DICTIONARY : if you want the dictionary to compare its KEYS in a way that its different from normal, you have to tell upfront at initialization time.
            //Reason is, inorder to give you the super efficient lookup, the dictionary needs to take into account how you want to lookup the keys when it's decides how to store its values internally.
            //it is done by passing the dectionary an "EQUALITY COMPARER"
            //EQUALITY COMPARER >Object that knows how to test for equality.
            //EQUALITY COMPARER implements IEqualityComparer<T>

            this.AllCountriesByKey = AllCountries.ToDictionary(x => x.Code,StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("-------------SEARCH COUNTRY (usa) case insensitive------------------");
            this.AllCountriesByKey.TryGetValue("usa", out Country result2);//case insensitive
            Console.WriteLine(result);

            Console.WriteLine("------------------------------------------");
            this.AllCountries = reader.ReadAllCountries().OrderBy(x => x.Name).ToList();
            this.AllCountriesByKey = AllCountries.ToDictionary(x => x.Code);
            //The order of the values in the dictionary is unspecified
            //you can't rely on the dictionary enumeration order
            //order works here since the list of sorted.so dictionary showed same order.
            //But there is no guarantee that it(order) is always be true with dictionary.since dictionary doesn't have any intrinsic order.
            //Relying on Dictionary order while enumerating is "not recommended".
            //Remedy: use SORTED DICTIONARY

            //-------------------------------------------------------------------------------------------------------
            //SortedDictionary<TKey,TValue>  
            //Keyed access to items 
            //automatically sorts items as it added to dictionary
            //Guaranteed sorted order when enumerating
            this.AllCountries = reader.ReadAllCountries().OrderBy(x => x.Name).ToList();
            var dict= AllCountries.ToDictionary(x => x.Code, StringComparer.OrdinalIgnoreCase);//List to Dictionary
            this.AllCountriesByKey1 = new SortedDictionary<string, Country>(dict);//Dictionary to Sorted Dictionary
            //SortedDictionary sorts dictionary by KEY
            foreach (var item in AllCountriesByKey1)
            {//This enumeration is sorted by KEY
                Console.WriteLine($"{item.Key} : {item.Value}");
            }


            //----------------------------------------------------------------------------
            //this.AllCountriesByKey1=new SortedDictionary<string, Country>(dict);
            //OR
            //this.AllCountriesByKey1 = new SortedList<string, Country>(dict);

            //SORTEDLIST  is functionally exactly same as SORTEDdICTIONARY
            //difference in performance characteristics
            //SORTEDLIST uses less memory but scales worse for inserting and removing items
            //SORTEDdICTIONARY modifications scales better. O(logN) vs O(N)

            //always prefer SORTEDdICTIONARY

            //---------------------CUSTOM TYPE-----------CountryCode-------------
            Console.WriteLine("---------------------CUSTOM TYPE-----------CountryCode-------------");
            CsvReader1 reader1 = new CsvReader1(@".\Resources\PopByLargest.csv");
            this.AllCountries1   = reader1.ReadAllCountries().OrderBy(x => x.Name).ToList();
            var dict1 = AllCountries1.ToDictionary(x => x.Code);//StringComparer can't be used as Code is not string.it is CountryCode custom type
            this.AllCountriesByKey3 = dict1;
            this.AllCountriesByKey3.TryGetValue(new CountryCode("usa"), out Country1 country1);
            Console.WriteLine(country1);//empty   
                                        //since it doesn't know how to compare "CountryCodes"  //since CountryCode is ref type is compare references.  //string is also a ref type but microsofts overridden that comparition to compare value 
                                        //country1 works only if Equals,GetHashcode is implemented for "CountryCode"

            //Dictionary key can be any type. //generally string is used
            //to use customtype as key, you must override Equals & GetHashcode

        }
    }
}
