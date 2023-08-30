using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Model
{
    public class Skill
    {
        public int Id { get; set; }
        Define.GameEnum.eSkillType type { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Damage { get; set; }


    }
}
