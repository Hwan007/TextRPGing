using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define;

namespace TextRPGing.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Define.GameEnum.eItemType Type { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public float CRT { get; set; }
        public float AVD { get; set; }
        public int Heal { get; set; }

        public Item()
        {

        }
        public Item(string name, GameEnum.eItemType type, int atk, float crt, int def, float avd, string description, int price)
        {
            Name = name;
            Type = type;
            ATK = atk;
            DEF = def;
            AVD = avd;
            CRT = crt;
            Description = description;
            Price = price;
        }

        public Item(int id, string name, GameEnum.eItemType type, string description, int price)
        {
            Id = id;
            Name = name;
            Type = type;
            Description = description;
            Price = price;
        }
    }
}
