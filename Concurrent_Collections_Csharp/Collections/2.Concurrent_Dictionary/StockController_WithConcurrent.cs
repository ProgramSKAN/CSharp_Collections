using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Collections._2.Concurrent_Dictionary
{
    public enum SelectResult { Success, NoStockLeft, ChosenShirtSold }
    public class StockController_WithConcurrent
    {
        //private Dictionary<string, TShirt> _stock;
        private ConcurrentDictionary<string, TShirt> _stock;
        public StockController_WithConcurrent(IEnumerable<TShirt> shirts)
        {
            //_stock = shirts.ToDictionary(x => x.Code);
            _stock = new ConcurrentDictionary<string, TShirt>(shirts.ToDictionary(x => x.Code));
        }
        public bool Sell(string code)
        {
            //_stock.Remove(code);
            return _stock.TryRemove(code,out TShirt shirtRemoved);
        }
        //public TShirt SelectRandomShirt()
        public (SelectResult Result,TShirt shirt) SelectRandomShirt()
        {
            var keys = _stock.Keys.ToList();
            if (keys.Count == 0)
                return (SelectResult.NoStockLeft,null); //all Shirts sold

            Thread.Sleep(Rnd.NextInt(10));//sleep time randomely between 0ms & 10ms
            string selectedCode = keys[Rnd.NextInt(keys.Count)];
            //return _stock[selectedCode];//in concurrentdictionary, selectedCode might not present in _stock.this is because when "var keys" List is set and thread slept, at that time other thread  sold the shirt and removed data from dictionary. 
            bool found = _stock.TryGetValue(selectedCode, out TShirt shirt);
            if (found)
                return (SelectResult.Success, shirt);
            else
                return (SelectResult.ChosenShirtSold, null);//shirt already sold by some other thread
        }
        public void DisplayStock()
        {
            Console.WriteLine($"\r\n{_stock.Count} items left in stock");
            foreach (TShirt shirt in _stock.Values)
                Console.WriteLine(shirt);
        }
    }
}
