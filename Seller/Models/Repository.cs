using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seller.Models
{
    public class Repository
    {
        private static Item[] _Items = new Item[] {
            new(1, "Nike Shoes", 75, 100),
            new(2, "Button Shirt", 30, 129),
            new(3, "Nice Hat", 49, 96),
            new(4, "Pants", 35, 123),
            new(5, "Glasses", 90, 100),
        };

        public static IEnumerable<Item> GetItems() => _Items;

        public static Item GetById(int id) => _Items.FirstOrDefault(i => i.Id == id);

        public static bool RemoveItem(string name)
        {
            var item = _Items.FirstOrDefault(i => i.Name == name);
            if(item == null || item.Qty < 1) 
                return false;

            item.Qty --;

            return true;
        }
        public static void Print(Action<string> print)
        {
            foreach (var item in _Items)
            {
                print(item.ToString());
            }
        }
    }
}
