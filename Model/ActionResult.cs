using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;

namespace TextRPGing.Model
{
    public class ActionResult
    {
        IAction action;
        public string Name { get; set; }

        public ActionResult Result { get; set; }
        public ActionResult()
        {

        }

        public ActionResult GetDamage(int atk, int def, float crt, float avd)
        {
            // 데미지 계산식
            throw new NotImplementedException();
        }
    }
}
