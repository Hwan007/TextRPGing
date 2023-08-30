using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;

namespace TextRPGing.Scene
{
    internal class Battle : IScene
    {

        Define.GameEnum.eSceneType type = Define.GameEnum.eSceneType.Battle;
        public void MainLoop()
        {

        }

        public bool ActByInput(int Input)
        {
            return false; // 임시로 써놓은겁니다.
        }
    }
}
