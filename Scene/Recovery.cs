using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;

namespace TextRPGing.Scene
{
    public class Recovery : IScene
    {
        Define.Enum.eSceneType type = Define.Enum.eSceneType.Recovery;
        public void MainLoop()
        {
            string message = "어서오세요, 스파르타 여관입니다!\n\n이곳에서는 지친 몸을 회복하실 수 있습니다.\n\n회복하시겠습니까?\n\n\n\n\n1. 회복하기\n0. 나가기";
            //출력
            if(int.TryParse(Console.ReadLine(), out int playerInput))
            {
                ActByInput(playerInput);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다!");
            }
        }
        public void ActByInput(int input)
        {
            
        }
    }
}
