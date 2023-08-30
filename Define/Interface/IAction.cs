using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Model;

namespace TextRPGing.Define.Interface
{
    public class IAction
    {
        string Name { get; set; }
        ActionResult result { get; set; }

        ActionResult GetDamage(int atk, int def, float crt, float avd);
    }

}
