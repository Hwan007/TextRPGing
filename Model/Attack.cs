using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;
using TextRPGing.Scene;

namespace TextRPGing.Model
{
    public class Attack : IAction
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public string AfterAction { get; set; }
        public int FinalDamage { get; set; }

        public Attack(int finalDamage)
        {
            Name = "일반 공격";
            Damage = finalDamage;
        }


        public int GetDamage(int atk, int def, float crt, float avd)
        {
            FinalDamage = atk;
            return FinalDamage;
        }
    }
}
