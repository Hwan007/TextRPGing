using System.Collections.Generic;

namespace TextRPGing.Model
{
    public class Inventory
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

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
