using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Collections._2.Concurrent_Dictionary
{
    public class StockController
    {
        private Dictionary<string, TShirt> _stock;//making _stock private is good encapsulation practice.Also restricting visibility is especially useful in multiple threads.

        public StockController(IEnumerable<TShirt> shirts)
        {
            _stock = shirts.ToDictionary(x => x.Code);
        }
        public void Sell(string code)
        {
            _stock.Remove(code);
        }
        public TShirt SelectRandomShirt()
        {
            var keys = _stock.Keys.ToList();//it is a SNAPSHOT of _stock at the moment this statement runs.
            if (keys.Count == 0)
                return null; //all Shirts sold

            Thread.Sleep(Rnd.NextInt(10));//sleep time randomely between 0ms & 10ms
            string selectedCode = keys[Rnd.NextInt(keys.Count)];
            return _stock[selectedCode];
        }
        public void DisplayStock()
        {
            Console.WriteLine($"\r\n{_stock.Count} items left in stock");
            foreach (TShirt shirt in _stock.Values)//it is a SNAPSHOT of _stock at the moment this statement runs.so it won't see any changes for _stock inside foreach
                Console.WriteLine(shirt);
        }
    }
}
//_stock.Keys, _stock.Values are aggregate properties which are expensive.Normally its okay if not invoked too often.
