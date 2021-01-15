using Collections.Extensions;
using Collections.Models;
using Collections.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collections
{
    //----------------LINKEDLISTS-------------------
    //Definite order, scalable insertion & deletion
    //Fast Changes,Slow Lookup    //list is just opposite
    //Efficient for adding and removing elements
    //complex
    //no direct element lookup
    //Have to wrap each element in LinkedListNode<T>
    //Optimized for faster changes
    //it has not requirement to store items sequentially in memory
    //not for looking up item. O(N)
    //usually copy LL to another collection when done editing.

    //List also have definite order.optimized for lookup O(1) but terrible for modifications due to O(N) 
    public class Coll_LinkedLists
    {
        public List<Country1> AllCountries { get; private set; }
        public Dictionary<CountryCode, Country1> AllCountriesByKey { get; private set; }
        public LinkedList<Country1> ItineraryBuilder { get; } = new LinkedList<Country1>();


        //to store ItineraryBuilder into AllTours
        //use SortedDictionary : to lookup tours by name,unique key automatically enforces unique tour names.it also enumerates tours by name
        public SortedDictionary<string, Tour> AllTours { get; private set; }
            = new SortedDictionary<string, Tour>();

        public void Run()
        {
            CsvReader1 reader = new CsvReader1(@".\Resources\PopByLargest.csv");
            this.AllCountries = reader.ReadAllCountries().OrderBy(x => x.Name).ToList();
            var dict = AllCountries.ToDictionary(x => x.Code);
            this.AllCountriesByKey = dict;

            //--------ADD----------
            Country1 selectedCountry = AllCountries[4];

            //Add at the end of LL
            this.ItineraryBuilder.AddLast(selectedCountry); //O(1)
            this.ItineraryBuilder.AddLast(AllCountries[14]); //O(1)
            this.ItineraryBuilder.AddLast(AllCountries[24]); //O(1)
            this.ItineraryBuilder.AddLast(AllCountries[34]); //O(1)
            foreach (var item in this.ItineraryBuilder)
            {
                Console.WriteLine($"{item.Code}:{item.Name}");
            }

            Console.WriteLine("----------------------REMOVE------------------------------");
            var nodeToRemove = this.ItineraryBuilder.GetNthNode(1);
            this.ItineraryBuilder.Remove(nodeToRemove);
            foreach (var item in this.ItineraryBuilder)
            {
                Console.WriteLine($"{item.Code}:{item.Name}");
            }

            Console.WriteLine("----------------------INSERT------------------------------");
            this.AllCountriesByKey.TryGetValue(new CountryCode("USA"),out Country1 result);
            var insertBeforeNode = this.ItineraryBuilder.GetNthNode(1);
            this.ItineraryBuilder.AddBefore(insertBeforeNode, result);
            foreach (var item in this.ItineraryBuilder)
            {
                Console.WriteLine($"{item.Code}:{item.Name}");
            }

            Console.WriteLine("----------------------INSERT likedlist to array------------------------------");
            string tourName = "MyTour";
            Country1[] itinerary = this.ItineraryBuilder.ToArray();
            try
            {
                Tour tour = new Tour(tourName, itinerary);
                this.AllTours.Add(tourName, tour);//sorteddictionary throws exception if tourName already exists.(enforce unique names)
            }
            catch (Exception)
            {
                Console.WriteLine("cannot save tour");
            }
            this.ItineraryBuilder.Clear();//clear linked list

            foreach (var item in this.AllTours)
            {
                Console.WriteLine($"{item.Key}:");
                foreach (var value in this.AllTours[item.Key].Itinerary)
                {
                    Console.WriteLine(value);
                }
            }
            
        }
    }
}
