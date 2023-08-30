using System;
using System.Collections.Generic;
using System.Text;
using TextRPGing.Utils;

namespace TextRPGing.Manager
{
    public class UIManager
    {
        private Queue<string> _messageQueue;

        public void DisplayUpdate()
        {
            if (_messageQueue.Count == 0)
                return;

            StringBuilder sb = new StringBuilder();
            foreach (string message in _messageQueue)
                sb.Append(message);

            Console.WriteLine(sb.ToString());
        }

        public void PutToOutQueue(MessageToUI message)
        {
            if (message == null)
                return;

            string[] cOut = message.COut;

            if (cOut.Length == 0) 
                return;

            foreach (string c in cOut)
                _messageQueue.Enqueue(c);
        }

        public void ConsoleClear()
        {
            Console.Clear();
        }

        public void ClearMessageQueue()
        {
            if (_messageQueue.Count == 0)
                return;

            _messageQueue.Clear();
        }


        public int GetMessageQueueCount()
        {
            return _messageQueue.Count;
        }
    }
}
