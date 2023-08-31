using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Model
{
    public class Weapon : Item
    {
        public int ATK { get; set; }
        public float CRT { get; set; }
        public Weapon(string name, string description, int atk, float crt, int price)
        {
            Name = name;
            Description = description;
            ATK = atk;
            CRT = crt;
            Price = price;
            Type = Define.GameEnum.eItemType.Weapon;
        }
    }
}
