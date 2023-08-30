using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;

namespace TextRPGing.Model
{
    public class ActionResult : IAction
    {
        public string Name { get; set; }
        public ActionResult result { get; set; }

        public ActionResult GetDamage(int atk, int def, float crt, float avd)
        {
            return new ActionResult();// 임시로 써놓은 거예요. 이거 어디다 쓰는겁니까?
        }
    }
}
