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
        private Queue<string> _messages = new Queue<string>();


        public void DisplayUpdate(Queue<string> messages)
        {

        }

        public void PutToOutQueue(MessageToUI message)
        {
            if (message == null)
                return;


            _messages.Enqueue();
        }

        public void DisplayClear()
        {
            _messages.Clear();
        }

    }
}
