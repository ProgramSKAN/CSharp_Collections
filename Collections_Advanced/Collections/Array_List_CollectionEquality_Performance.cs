using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collections
{
    public class Array_List_CollectionEquality_Performance
    {
        public static void Run()
        {
			DateTime[] bankHols1 =
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
			//bankHols1.Add();//invalid //arrays are fixed size
			bankHols1[0] = new DateTime(2021, 4, 1);//modify.  //knowing index is must
													//why? >performance
													//Arrays under the hood stored "sequentially" in "single block of memory"
													//easy to locate item using index
													//fixed size is performant, because if the size increases later it has to search for new block of size in memory and move all the elements.
													//most collections use arrays under the hood.

			DateTime[] bankHols2 =
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
			DateTime[] bankHols3 = bankHols1;

			//EQUALITY
			Console.WriteLine($"bn1==bh2? {bankHols1 == bankHols2}");//false  //datetime is ref type.diff addresses
			Console.WriteLine($"equal values? {bankHols1.SequenceEqual(bankHols2)}");//true   //expensive operstion
			Console.WriteLine($"bn2==bh3? {bankHols2 == bankHols3}");//false  //diff references
			Console.WriteLine($"bn3==bh1? {bankHols3 == bankHols1}");//true   //same reference


			List<DateTime> bankHols4 = new List<DateTime>()
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
			bankHols4.Add(new DateTime(2021, 4, 1));
			//Lists can add new item, but less performant than array.
			//Lists uses array.allocates memory slightly more than required.so, Add() possible.
			//if added many items then if old memory fills it will get new memory block >new size then copy old array.reduces performance.

		}
	}
}
