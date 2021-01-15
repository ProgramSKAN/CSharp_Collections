using Collections.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Collections.Resources
{
    public class CsvReaderWithList
    {
		private string _csvFilePath;

		public CsvReaderWithList(string csvFilePath)
		{
			this._csvFilePath = csvFilePath;
		}

		public List<Country> ReadAllCountries()
		{
			List<Country> countries = new List<Country>();

			using (StreamReader sr = new StreamReader(_csvFilePath))
			{
				// read header line
				sr.ReadLine();

				string csvLine;
				while ((csvLine = sr.ReadLine()) != null)
				{
					countries.Add(ReadCountryFromCsvLine(csvLine));
				}
			}

			return countries;
		}

		public Country ReadCountryFromCsvLine(string csvLine)
		{
			string[] parts = csvLine.Split(',');
			string name;
			string code;
			string region;
			string popText;
			switch (parts.Length)
			{
				case 4:
					name = parts[0];
					code = parts[1];
					region = parts[2];
					popText = parts[3];
					break;
				case 5:
					name = parts[0] + ", " + parts[1];
					name = name.Replace("\"", null).Trim();
					code = parts[2];
					region = parts[3];
					popText = parts[4];
					break;
				default:
					throw new Exception($"Can't parse country from csvLine: {csvLine}");
			}

			// TryParse leaves population=0 if can't parse
			int.TryParse(popText, out int population);
			return new Country(name, code, region, population);
		}

		//public void RemoveCommaCountries(List<Country> countries)
		//{
		//	for (int i =0; i< countries.Count; i++)
		//	{
		//		if (countries[i].Name.Contains(','))
		//			countries.RemoveAt(i);
		//	}
		//}//buggy. because, if we remove element at index i and increment i, the element after i shifts up.so there will be a skip for 1 item everytime you remove.
		//Remedy :1.don't increment i when item removed. or 2.iterate from end instead of start(like below)

		public void RemoveCommaCountries(List<Country> countries)
		{
			// This removes countries with commas using the RemoveAll() method
			// countries.RemoveAll(x => x.Name.Contains(','));
			//OR
			// this removes countries with commas in a for loop (correctly, counting backwards)
			for (int i = countries.Count - 1; i >= 0; i--)
			{
				if (countries[i].Name.Contains(','))
					countries.RemoveAt(i);
			}
		}





	}
}
