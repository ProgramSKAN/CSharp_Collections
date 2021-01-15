using Collections.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collections
{
    //---------------------Stack<T>------------------------------
    //for situations where list of items/tasks to be processed
    //new itesm are added to collection
    //item removed as they are processed
    //PUSH & POP   |  LIFO
    //Stack are ordered
    //no direct lookup
    
    //Ex: undo feature / Call stack
    public class Coll_Stack
    {
		public List<Country> AllCountries { get; private set; }
		public Dictionary<CountryCode, Country> AllCountriesByKey { get; private set; }
		public LinkedList<Country> ItineraryBuilder { get; } = new LinkedList<Country>();
		public SortedDictionary<string, Tour> AllTours { get; private set; }
			= new SortedDictionary<string, Tour>();
		public Stack<ItineraryChange> ChangeLog { get; } = new Stack<ItineraryChange>();
		public void Run()
        {
			CsvReader reader = new CsvReader(@".\Resources\PopByLargest.csv");
			this.AllCountries = reader.ReadAllCountries().OrderBy(x => x.Name).ToList();
			var dict = AllCountries.ToDictionary(x => x.Code);
			this.AllCountriesByKey = dict;

            Console.WriteLine("-----------------ADD-------------------------");
			Country selectedCountry = AllCountries[4];
			Country selectedCountry1 = AllCountries[14];
			Country selectedCountry2 = AllCountries[24];
			Country selectedCountry3 = AllCountries[34];
            //Add at the end of LL
            this.ItineraryBuilder.AddLast(selectedCountry);
            var change = new ItineraryChange(ChangeType.Append, this.ItineraryBuilder.Count, selectedCountry);
            this.ChangeLog.Push(change);

            this.ItineraryBuilder.AddLast(selectedCountry1);
            var change1 = new ItineraryChange(ChangeType.Append, this.ItineraryBuilder.Count, selectedCountry1);
            this.ChangeLog.Push(change1);

            this.ItineraryBuilder.AddLast(selectedCountry2);
            var change2 = new ItineraryChange(ChangeType.Append, this.ItineraryBuilder.Count, selectedCountry2);
            this.ChangeLog.Push(change2);

            this.ItineraryBuilder.AddLast(selectedCountry3);
            var change3 = new ItineraryChange(ChangeType.Append, this.ItineraryBuilder.Count, selectedCountry3);
            this.ChangeLog.Push(change3);

            foreach (var item in this.ItineraryBuilder)
            {
                Console.WriteLine($"{item.Code}:{item.Name}");
            }
            Console.WriteLine("---stack----");
            foreach (var item in ChangeLog)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("-----------------REMOVE-------------------------");
            int selectedIndex = 1;
            var nodeToRemove = this.ItineraryBuilder.GetNthNode(selectedIndex);
            this.ItineraryBuilder.Remove(nodeToRemove);
            var change4 = new ItineraryChange(ChangeType.Remove, selectedIndex, nodeToRemove.Value);
            this.ChangeLog.Push(change4);
            foreach (var item in this.ItineraryBuilder)
            {
                Console.WriteLine($"{item.Code}:{item.Name}");
            }
            Console.WriteLine("---stack----");
            foreach (var item in ChangeLog)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("----------------------INSERT------------------------------");
            int selectedIndex1 = 1;
            this.AllCountriesByKey.TryGetValue(new CountryCode("USA"), out Country result);
            var insertBeforeNode = this.ItineraryBuilder.GetNthNode(selectedIndex1);
            this.ItineraryBuilder.AddBefore(insertBeforeNode, result);
            var change5 = new ItineraryChange(ChangeType.Insert, selectedIndex1, insertBeforeNode.Value);
            this.ChangeLog.Push(change5);
            foreach (var item in this.ItineraryBuilder)
            {
                Console.WriteLine($"{item.Code}:{item.Name}");
            }
            Console.WriteLine("---stack----");
            foreach (var item in ChangeLog)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("---------------------UNDO-------------------------------");
            ItineraryChange lastChange = this.ChangeLog.Pop();
            ChangeUndoer.Undo(this.ItineraryBuilder, lastChange);
            ItineraryChange lastChange2 = this.ChangeLog.Pop();
            ChangeUndoer.Undo(this.ItineraryBuilder, lastChange2);
            ItineraryChange lastChange3 = this.ChangeLog.Pop();
            ChangeUndoer.Undo(this.ItineraryBuilder, lastChange3);
            ChangeUndoer.Undo(this.ItineraryBuilder, this.ChangeLog.Pop());

            foreach (var item in this.ItineraryBuilder)
            {
                Console.WriteLine($"{item.Code}:{item.Name}");
            }
            Console.WriteLine("---stack----");
            foreach (var item in ChangeLog)
            {
                Console.WriteLine(item);
            }


        }
    }
}
