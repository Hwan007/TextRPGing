using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Model
{
    public class Potion : Item
    {
        public int Heal { get; set; }

        public Potion()
        {
            Name = "회복 물약";
            Description = "흔하게 볼 수 있는 회복 물약입니다.";
            Type = Define.GameEnum.eItemType.Potion;
            Price = 100;
        }
    }
}
