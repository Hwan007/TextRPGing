using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define;
using TextRPGing.Define.Interface;
using TextRPGing.Manager;
using TextRPGing.Model;
using TextRPGing.Utils;

namespace TextRPGing.Scene
{
    public class SaveLoad : IScene
    {
        private Define.GameEnum.eSceneType type = Define.GameEnum.eSceneType.SaveLoad;

        public bool ActByInput(int input, ref GameEnum.eSceneType scene)
        {
            // 1 . 저장하기
            // 2 . 불러오기
            // 3. 다른 씬으로 이동

            switch (input)
            {
                case 1:
                    IOStream.SavePlayerJsonFile();
                    return true;
                case 2:
                    Character.Player = IOStream.LoadPlayerJsonFile();
                    return true;
                case 3:
                    string message = AppendRoutes().ToString();
                    MessageToUIManager(message);
                    break;

            }
            return false;
        }

        public void MainLoop()
        {
            DisplaySaveLoadScene();
        }

        private void DisplaySaveLoadScene()
        {
            GameManager.UIManager.ConsoleClear();
            string message = "저장 및 불러오기\n" +
                                   "\r\n" +
                                   "1. 저장하기\r\n" +
                                   "2. 불러오기\r\n" +
                                   "3. 다른 씬으로 이동\r\n" +
                                   "\r\n" +
                                   "원하시는 행동을 입력하세요.\r\n" +
                                   ">> ";

            MessageToUIManager(message);
        }


        private StringBuilder AppendRoutes()
        {
            StringBuilder sb = new StringBuilder();
            Define.GameEnum.eSceneType[] routes = GameManager.SceneManager.GetEnableScene(type);
            for (int i = 0; i < routes.Length; ++i)
            {
                sb.Append($"{i}. ");
                switch (routes[i])
                {
                    case Define.GameEnum.eSceneType.Status:
                        sb.Append($"상태보기\n");
                        break;
                    case Define.GameEnum.eSceneType.Battle:
                        sb.Append($"전투 시작\n");
                        break;
                    case Define.GameEnum.eSceneType.Recovery:
                        sb.Append($"포션 사용\n");
                        break;
                    case Define.GameEnum.eSceneType.SaveLoad:
                        break;
                    default:
                        sb.Append($"오류입니다. 수정해주세요.\n");
                        break;
                }
            }
            sb.Append("\n");
            return sb;
        }



        private void MessageToUIManager(string message)
        {
            MessageToUI messageToUi = new MessageToUI(
                Define.GameEnum.eSceneType.Recovery,
                new string[] { message }
            );

            GameManager.UIManager.PutToOutQueue(messageToUi);
            GameManager.UIManager.DisplayUpdate();
            GameManager.UIManager.ClearMessageQueue();
        }
    }
}
