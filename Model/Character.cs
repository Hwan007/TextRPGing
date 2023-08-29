using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Controller;

namespace TextRPGing.Model
{
    internal class Character : CharacterController
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public float CRT { get; set; }
        public float AVD { get; set; }
        enum characterClass
        {
            Warrior,
            Thief,
            Archer,
            Magician
        }
        Inventory Inven { get; set; }
        Equipment Equip { get; set; }

    }
}
