using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.HashSet_and_SortedSet
{
    public enum ChangeType { Append, Insert, Remove}
    
    public class ItineraryChange
    {
        public ChangeType ChangeType { get; }
        public Country Value { get; }
        public int Index { get; }

        public ItineraryChange(ChangeType changeType, int index, Country countryRemoved)
        {
            this.ChangeType = changeType;
            this.Index = index;
            this.Value = countryRemoved;
        }
    }
}
