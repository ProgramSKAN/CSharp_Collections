using Collections.HashSet_and_SortedSet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            Array_List_CollectionEquality_Performance.Run();
            Collection_Performance.Run();

            Dictnries_and_SortedDictnries dic = new Dictnries_and_SortedDictnries();
            dic.Run();


            Coll_LinkedLists linkedLists = new Coll_LinkedLists();
            linkedLists.Run();

            Coll_Stack stack = new Coll_Stack();
            stack.Run();

            //-----Dictionaries,Stacks,Queues,LinkedLists,Arrays------ All of there collections are not Thread Safe, which causes data corruption-----------
            //ConcurrentQueue<T> does not have a Peek(), because you can't guarantee it would succeed.use TryPeek().



            //SETS
            //Enforce Uniqueness
            //easy to perform Operations on whole collections. Eg:Merging collections


            //Coll_Hash_Sorted_Set coll_Hash_Sorted_Set = new Coll_Hash_Sorted_Set();
            //coll_Hash_Sorted_Set.Run();
            //DICTIONARY,HASHSET  >must compare items for equality
            //SORTEDDICTIONARY,SORTEDLIST,SORTEDSET >must compare items for ordering 
            //not required T is builtin types like int,string.


            //-------------
            //For Code Robostness and prventing modifications use 'Read-Only' & 'Immutable' Collections
            //use system.collections.ObjectModel  > ReadOnlyDictionary<T> ,ReadOnlyCollection<T> is used for ReadOnlyList.
            //AllCountries,AllCountriesByKey  >  Read-only  (once initialized, they never change)
            Coll_ReadOnly_Collections coll_ReadOnly_Collections = new Coll_ReadOnly_Collections();
            coll_ReadOnly_Collections.Run(); //with lilliput added

            Coll_immutable_Collections coll_Immutable_Collections = new Coll_immutable_Collections();
            coll_Immutable_Collections.Run(); //without lilliput added


            //IMMUTABLE COLLECTIONS  >Thread safe (due to immutable)
            //STANDARD COLLECTIONS   >not thred safe (no internal syncronization)
            //CONCURRENT COLLECTIONS > Thread safe (internal thread syncronization)

            //So, in Multi Threading
            //use IMMUTABLE COLLECTIONS for reading
            //use CONCURRENT COLLECTIONS for writing
            //don't use STANDARD COLLECTIONS

            //----------------------------
            //Collection Interfaces
            //Decouples Code
            //makes code easy to maintain
            //more efficient code

            //List<T>  implements ICollection,IEnumerable,IList,IReadOnlyCollection,IReadOnlyList,IList
            /*
                 ---------------------IEnumerable<T>**--------------------
                      ^                      ^                     ^
                ICollection<T>       IReadOnlyCollection<T>      IList
                      ^                        ^
                    IList<T>**             IReadOnlyList<T>**
            */
            //similarly
            //IEnumerable<KeyValuePair<TKey,TValue>>     <-------IDictionary<Tk,Tv>,  IReadOnlyDictionary<Tk,Tv>

            //all collections implement IEnumerable<T> for enumeration of their items.
            //for each uses GetEnumerator() to get the enumeration of T
            //LINQ also uses IEnumerable<T>. ex: var x= from y in myCollection..

            //------------------------------------------
            //List<Tour> getRequestedTours = selectedItems.Cast<Tour>().ToList();

            //LINQ does just-in-time Enumeration.ie, it never actually enumerates anything until it absolutely has to .
            //selectedItems.Cast<Tour>()  doesnt actually enumerate th Tours instead it returns some object that knows how to grab the selected tours on demand and enumerates them when it actually asked to do so.
            //.ToList() enumerates the tours to create actual collection.
            //But, we don't really need the collection List. we just need to be able to enumerate.
            //so, .ToList() is a waste of machine cycle.

            //IEnumerable<Tour> getRequestedTours = selectedItems.Cast<Tour>();
            //this is more efficient. Returning an interface saves building the extra collection.

            //like List , IEnumerable don't have 'Count()'.so use Any()
            //LINQ  Any()  >checks whether an enumerable sequence contains any elements

            //instead of 
            //    if(getRequestedTours.count==0)
            //use
            //      if(!getRequestedTours.Any())

            //if count of getRequestedTours is must , then use .ToList() like before.
            //-----------------------------------------
            //ImmutableArray<T> implements IReadOnlyList<T> , IImutableList<T>
            //Array and List share same interfaces

            //IReadOnlyList  >gives wrapper read access to array/list
            //IImutableList  >gives truly read access to array/list and Extra immutable features like "memory-shared collections"


            //in Coll_immutable_Collections.cs
            //public List<Country> AllCountries { get; private set; }
            //if it changed to 
            //public IReadOnlyList<Country> AllCountries { get; private set; }
            //then

            //this.AllCountries = countris.ToImmutableArray(); //or
            //this.AllCountries = countris.ToArray();

            //AllCountries can be any type that implements IReadOnlyList
            //.ToArray()  is LINQ extension method to make array form any enumerable sequence.
        }
    }
}
