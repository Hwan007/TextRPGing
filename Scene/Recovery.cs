using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;
using TextRPGing.Manager;
using TextRPGing.Utils;

namespace TextRPGing.Scene
{
    public class Recovery : IScene
    {
        Define.GameEnum.eSceneType type = Define.GameEnum.eSceneType.Recovery;
        UIManager _uiManager = new UIManager();
        public void MainLoop()
        {
            DisplayRecoveryScene();        


        }
        public bool ActByInput(int input)
        {
            if (input == 1)
            {

            }
            return false;
        }

        private void DisplayRecoveryScene()
        {
            _uiManager.ConsoleClear();
            string message = "회복\n아이템을 사용해 체력을 30 회복할 수 있습니다.\n소지개수 : {개수}\n\n체력 : \n\n\n\n\n1. 회복\n0. 나가기";

            MessageToUIManager(message);
        }


        private void MessageToUIManager(string message)
        {
            MessageToUI messageToUi = new MessageToUI(
                Define.GameEnum.eSceneType.Main,
                new string[] { message }
            );

            _uiManager.PutToOutQueue(messageToUi);
            _uiManager.DisplayUpdate();
            _uiManager.ClearMessageQueue();
        }

    }
}
