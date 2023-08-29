using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Define.Interface
{
    public interface IBattleStat
    {
        string Name { get; set; }
        int HP { get; set; }
        int MaxHP { get; set; }
        int ATK { get; set; }
        int DEF { get; set; }
        float CRT { get; set; }
        float AVD { get; set; }
    }
}
