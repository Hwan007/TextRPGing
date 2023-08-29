using System;
using TextRPGing.Manager;

namespace TextRPGing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.GameStart();
        }
    }
}
