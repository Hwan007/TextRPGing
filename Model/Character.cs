using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Controller;
using TextRPGing.Define.Interface;

namespace TextRPGing.Model
{
    public class Character : CharacterController, IBattleStat
    {
        public static Character Player = null;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public float CRT { get; set; }
        public float AVD { get; set; }
        public Inventory Inven { get; set; }
        public Equipment Equip { get; set; }

        private int xATK = 0;
        private int xDEF = 0;

        public override void ReStat()
        {
            ATK -= xATK;
            xATK = 0;
            DEF -= xDEF;
            xDEF = 0;
            foreach (Weapon weapon in Equip.Items)
            {
                xATK += weapon.ATK;
            }
            foreach (Armor armor in Equip.Items)
            {
                xDEF += armor.DEF;
            }
            ATK += xATK;
            DEF -= xDEF;
        }

        public override void TakeDamage(int  damage)
        {
            HP -= (int)((damage - DEF) * AVD); //임의로 데미지 계산 식 만들어놨습니다. 마음껏 변경하셔도 됩니다.
        }

        public override void TakeHeal(int heal)
        {
            HP += heal;
            if (HP > MaxHP)
                HP = MaxHP;
        }

    } 
}
