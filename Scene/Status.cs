using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Manager;

namespace TextRPGing.Scene
{
    public class Status : Define.Interface.IScene
    {
        public bool ActByInput(int input)
        {
            return false;
        }

        public void MainLoop()
        {
            // 타이틀 출력
            GameManager.UIManager

            // 캐릭터 정보 출력

            // 선택지 출력
        }
    }
}
