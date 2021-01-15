using Collections.Models;
using Collections.Resources;
using System;
using System.Collections.Generic;
using System.Text;
/*
------------ARRAY-----------
characteristics:
1.fixed number of items
2.definite order  >Arrays store and enumerate items in definite order
3.array can be made of any type

Ex. Days of a week  >there are 7 days and always be 7 days(fixed size). Days have specific order (Mon,Tues,...)


ELEMENT / ITEM :An object (or struct) in a collection.
ENUMERATE / ITERATE : Go through each item in turn.

*for arrays, you always specify the item you want with "number"(index) as [1].because arrays are ordered.
*Arrays are 0-based-Indexed
*if you lookup for 7th index in array of size 6 then >IndexOutOfRangeException   >Collections are safe against bad lookups
*/
namespace Collections
{
    public class Coll_Arrays
    {
        public static void Run()
        {
            //ARRAY
            string[] daysOfWeek =
            {
                "Monday",
                "Tuesday",
                "Wenesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };
            //{ }  >collection initializer

            //print all days in order
            //arrays are ordered
            Console.WriteLine("Before:");
            foreach (string day in daysOfWeek)
                Console.WriteLine(day);

            //Replacing Array Items
            daysOfWeek[2] = "Wednesday";

            Console.WriteLine("\r\nBefore:");
            foreach (string day in daysOfWeek)
                Console.WriteLine(day);


            //Array of int
            int[] ints = { 1, 2, 3, 4 };
            //Array of points
            System.Drawing.Point[] points = { new System.Drawing.Point(3, 5) };

            //LOOK OF AN ITEM IN ARRAY
            Console.WriteLine("Which day do you want to display?");
            Console.Write("(Monday = 1, etc.) > ");
            int iDay = int.Parse(Console.ReadLine());

            string chosenDay = daysOfWeek[iDay - 1];
            Console.WriteLine($"That day is {chosenDay}");

            //-------------------
            //READ DATA FROM EXTERNAL DATA SOURCE INTO AN ARRAY
            //dynamically put data in array
            //Arrays are Uniquitous (constantly encountered)
            //uninitialized array contains nulls (for reference types)
            //Arrays are normal reference types.(there can be null)
            //Arrays always start with full null/default values.(value is null(ref type) if not initialialized)
            string filePath = @".\Resources\Pop by Largest Final.csv";
            CsvReaderWithArray reader = new CsvReaderWithArray(filePath);

            Country[] countries = reader.ReadFirstNCountries(10);

            foreach (Country country in countries)
            {
                Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
            }


            //------------------------
            Country[] countries1 = null;//this is just declaration , not instantiation
            //int number = null;//invalid since it is value type
            int[] numbers = null;//valid since arrays are reference types

            //arrays are fixed size, so array cannot be created with specifying size.
            Country[] countries2 = new Country[10];//array with all nulls

            int[] ints1 = new int[10];//array with all 0's (int default value =0)

            int[] ints2 = new int[] { 1, 2, 3, 4 };//array initializer 
            //or
            int[] ints3 = { 1, 2, 3, 4 };
        }
    }
}





