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
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ActionResult Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Skill(int id, Define.GameEnum.eSkillType type, int minDamage, int maxDamage, int damage)
        {

        }

        public ActionResult GetDamage(int atk, int def, float crt, float avd)
        {
            throw new NotImplementedException();
        }
    }
}
