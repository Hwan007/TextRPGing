using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;
using TextRPGing.Controller;

namespace TextRPGing.Model
{
    public class Monster : MonsterController, IBattleStat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public float CRT { get; set; }
        public float AVD { get; set; }
        public int RewardExp { get; set; }
        public int RewardGold { get; set; }

        public Monster(string name, int maxHP, int atk, int def, float crt, float avd)
        {
            Name = name;
            HP = maxHP;
            MaxHP = maxHP;
            ATK = atk;
            DEF = def;
            CRT = crt;
            AVD = avd;
        }

        public override void TakeDamage(int damage)
        {
            HP -= (int)((damage - DEF) * AVD);
        }
    }
}
