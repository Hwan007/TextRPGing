using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define;

namespace TextRPGing.Model
{
    public class Potion : Item
    {

        public int Heal { get; set; }

        public Potion(int id, string name, GameEnum.eItemType type, string description, int price) : base(id, name, type, description, price)
        {
        }
    }
}
