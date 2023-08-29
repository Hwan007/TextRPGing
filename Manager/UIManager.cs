using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Utils;

namespace TextRPGing.Manager
{
    public class UIManager
    {
        private Queue<string> _messages ;


        public void DisplayUpdate(Queue<string> messages)
        {

        }

        public void PutToOutQueue(MessageToUI message)
        {
            if (message == null)
                return;

            string[] cOut = message.COut;

            if (cOut.Length == 0) 
                return;

            foreach (string c in cOut)
                _messages.Enqueue(c);
        }

        public void DisplayClear()
        {

        }

    }
}
