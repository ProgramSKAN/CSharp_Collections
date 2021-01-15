using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.HashSet_and_SortedSet
{
    //IComparer<T> declares that it knows how to order instances of T.
    //An Equality Comparer compares for equality.A Comparer compares for Ordering.
    public class CountryNameComparer : IComparer<Country>
    {
        public static CountryNameComparer Instance { get; } = new CountryNameComparer();
        private CountryNameComparer() { }
        public int Compare(Country x, Country y)//tests which of two items comes first in order
        {
            return x.Name.CompareTo(y.Name);//string.compareTo compares strings alphabetically
        }
    }
}
