using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seller.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Qty { get; set; }

        public Item(int id, string name, float price, int qty)
        {
            Id = id;
            Name = name;
            Price = price;
            Qty = qty;
        }

        public Item()
        {

        }

        public override string ToString() => $"{Id}: {Name} | ${Price} | {Qty}";
    }
}
