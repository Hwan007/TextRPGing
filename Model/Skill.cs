using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;

namespace TextRPGing.Model
{
    public class Skill : IAction
    {
        public int Id { get; set; }
        Define.GameEnum.eSkillType Type { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Damage { get; set; }

        public Skill(int id, Define.GameEnum.eSkillType type, int minDamage, int maxDamage, int damage)
        {

        }
    }
}
