using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Utils;

namespace TextRPGing.Manager
{
    public class GameManager
    {
        private SceneManager _sceneManager = new SceneManager();
        private UIManager _uiManager = new UIManager();

        public void GameStart()
        {
            DisplayIntroScene();

            while (true)
            {
                string input = Console.ReadLine();

                int inputNum = 0;
                if (int.TryParse(input, out inputNum) == false)
                {
                    DisplayValidInput();
                    continue;
                }

                bool isInputNumAct = _sceneManager.ActByInput(inputNum);

                if (isInputNumAct == false)
                {
                    DisplayValidInput();
                    continue;
                }
            }
        }

        private void DisplayIntroScene()
        {
            _uiManager.ConsoleClear();
            string message = "스파르타 던전에 오신 여러분 환영합니다.\r\n" +
                                  "이제 전투를 시작할 수 있습니다.\r\n" +
                                  "\r\n" +
                                  "1.상태 보기\r\n" +
                                  "2.전투 시작" +
                                  "\r\n" +
                                  "원하시는 행동을 입력해주세요.\r\n" +
                                  ">> ";

            MessageToUIManager(message);
        }

        private void DisplayValidInput()
        {
            string message = "잘못된 입력입니다.";
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
