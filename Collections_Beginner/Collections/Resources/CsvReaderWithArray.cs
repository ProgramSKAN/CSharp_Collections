using Collections.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Collections.Resources
{
    public class CsvReaderWithArray
    {
		private string _csvFilePath;

		public CsvReaderWithArray(string csvFilePath)
		{
			this._csvFilePath = csvFilePath;
		}

		public Country[] ReadFirstNCountries(int nCountries)
		{
			Country[] countries = new Country[nCountries];

			using (StreamReader sr = new StreamReader(_csvFilePath))
			{
				// read header line  //population from world bank data
				sr.ReadLine();

				for (int i = 0; i < nCountries; i++)
				{
					string csvLine = sr.ReadLine();
					countries[i] = ReadCountryFromCsvLine(csvLine);
				}
			}

			return countries;
		}

		public Country ReadCountryFromCsvLine(string csvLine)
		{
			//string[] parts = csvLine.Split(',');  //params char[]? separator   //or
			string[] parts = csvLine.Split(new char[] { ',' });

			string name = parts[0];
			string code = parts[1];
			string region = parts[2];
			int population = int.Parse(parts[3]);

			return new Country(name, code, region, population);
		}


	}
}
