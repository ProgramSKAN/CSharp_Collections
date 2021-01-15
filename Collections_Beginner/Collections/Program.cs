using Collections.Models;
using System;
using System.Collections.Generic;



namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            //Coll_Arrays.Run();
            //JAGGED ARRAY & MULTIDIMENTIONAL ARRAY https://app.pluralsight.com/guides/multidimensional-arrays-csharp
            //use MD array when each inner array size is eqaul. else use Jagged array.

            //Coll_Lists.Run();
            //Coll_Dictionaries.Run();
            //Linq_Utility.Run();

            //Collections in collections
            Dictionary<string, List<Country>> keyValuePairs = new Dictionary<string, List<Country>>();

            Coll_in_Coll_Dictry_List.Run();

            //GENERIC COLLECTIONS > List<T> , Dictionary<TKey,TValue> ,SortedList<>, SortedDictionary<> ,LinkedList<>(use for more frequent data insert/removal) 
            //Systems.Collections.Generic
            //Array is not standard generic collection  //it is into .NetRuntime
            //standard generic collections are not thread safe

            //CUSTOM COLLECTIONS > ObservableCollection in WPF
            //Systems.Collections.ObjectModel   


            //IMMUTABLE COLLECTIONS > ImmutableArray, ImmutableList, ImmutableDictionary
            //Cannot ever be modified, once instantiated
            //Immutable Collections are naturally Thread Safe.because there ever be modified.


            //CONCURRENT COLLECTIONS
            //Similar to standard collection, but these are designed to be thread safe

            //LINQ


            //COLLECTION INTERFACES
            //LINQ,foreach() implements IEnumerable<T>
            //T[], List<T> implements IList<T>
            Country[] countries = new Country[10];
            //OR
            IList<Country> countries1 = new Country[10];
            //both are same.functionally nothing has changed but the benifit is that somewhat decoupled the main method.since countries1 need not be just array, it can be List<Country>.

        }
    }
}
