using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define;
using TextRPGing.Define.Interface;

namespace TextRPGing.Model
{
    public class Skill : IAction
    {
        public int Id { get; set; }
        Define.GameEnum.eSkillType Type { get; set; }
        public int CrtDamage { get; set; }
        public int Damage { get; set; }


        public string Name { get; set; }
        public string AfterAction { get; set; }
        public int FinalDamage { get; set; }
        public Skill(int damage)
        {
            Name = Character.Player.Skill;
            Damage = damage;
        }

        public int GetDamage(int atk, int def, float crt, float avd)
        {
            throw new NotImplementedException();
        }
    }
}
