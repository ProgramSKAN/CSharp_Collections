using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Models
{
	//in real apps, this custom type would allow adding features to code.(like enforcing 3-letter codes)
    public class CountryCode
    {
		public string Value { get; }

		public CountryCode(string value)
		{
			Value = value;
		}

		public override string ToString() => Value;

		//this overide is mandatory for dictionary
		public override bool Equals(object obj)
        {
            if (obj is CountryCode other)
                return StringComparer.OrdinalIgnoreCase.Equals(this.Value, other.Value);
            return false;
        }

		//this overide is mandatory for dictionary
		//GetHashCode generates a hashcode(an integer that represents compressed version of the value of the object).Dictionaries rely on hashcode extensively for storing/lookup items.
		//dictionary lookup items fail if GetHashCode not implemented.so, override GetHashCode for "CountryCode" Type
		//GetHashCode implemetation: the requirement is "GetHashCode" has to work consistently with "object.Equals()"
		//so, Equals & GetHashCode both from "StringComparer"
		public override int GetHashCode() =>
			StringComparer.OrdinalIgnoreCase.GetHashCode(this.Value);


        //==,!==  overides are not mandatory for dictionary.  good practice
        public static bool operator ==(CountryCode lhs, CountryCode rhs)
        {
            if (lhs != null)
                return lhs.Equals(rhs);
            else
                return rhs == null;
        }
        public static bool operator !=(CountryCode lhs, CountryCode rhs)
        {
            return !(lhs == rhs);
        }
    }
}
