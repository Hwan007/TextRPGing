using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;

namespace TextRPGing.Model
{
    public class Attack : IAction
    {
        public int Damage { get; set; }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ActionResult Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Attack(int damage)
        {
            Damage = damage;
        }

        public ActionResult GetDamage(int atk, int def, float crt, float avd)
        {
            throw new NotImplementedException();
        }
    }
}
