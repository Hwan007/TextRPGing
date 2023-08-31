using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define;

namespace TextRPGing.Model
{
    public class Weapon : Item
    {

        public int ATK { get; set; }
        public float CRT { get; set; }
        public Weapon(int id, string name, GameEnum.eItemType type, string description, int price) : base(id, name, type, description, price)
        {
        }
    }
}
