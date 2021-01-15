using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Collections._5.BlockingCollection_and_ConsumingEnumerable
{
	public enum TradeType { Sale, Purchase}
	public class Trade
	{
		public SalesPerson Person { get; private set; }
		public TShirt Shirt { get; private set; }
		public int Quantity { get; private set; }
		public TradeType Type { get; private set; }
		public bool IsSale => Type == TradeType.Sale;

		public Trade(SalesPerson person, TShirt shirt, TradeType type, int quantitySold)
		{
			this.Person = person;
			this.Shirt = shirt;
			this.Type = type;
			this.Quantity = quantitySold;
		}
		public override string ToString()
		{
			string typeText = IsSale ? "bought" : "sold";
			return $"{Person} {typeText} {Quantity} {Shirt.Name}";
		}
	}
}
