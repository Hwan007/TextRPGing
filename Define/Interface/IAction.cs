using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Model;

namespace TextRPGing.Define.Interface
{
    public interface IAction
    {
        string AfterAction { get; set; }

        int FinalDamage { get; set; }

        abstract int GetDamage(int atk, int def, float crt, float avd);
    }
}
