using System.Collections.Generic;
using TextRPGing.Controller;

namespace TextRPGing.Model
{
    public class Inventory  : InventoryController
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public int Gold { get; set; }
        public Inventory()
        {
        }

        public Inventory(int id, List<Item> items)
        {
            Id = id;
            Items = items;
        }
    }
}
