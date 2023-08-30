using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define;

namespace TextRPGing.Utils
{
    public class MessageToUI
    {
        public Define.GameEnum.eSceneType Type { get; set; }
        public string[] COut { get; set; }

        public MessageToUI()
        {
        }

        public MessageToUI(GameEnum.eSceneType type, string[] cOut)
        {
            Type = type;
            COut = cOut;
        }
    }
}
