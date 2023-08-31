using System;
using System.Text;
using TextRPGing.Define;
using TextRPGing.Define.Interface;
using TextRPGing.Manager;
using TextRPGing.Model;
using TextRPGing.Utils;

namespace TextRPGing.Scene
{
    public class SaveLoad : IScene
    {
        private Define.GameEnum.eSceneType _type = Define.GameEnum.eSceneType.SaveLoad;
        private bool _isSaveLoadSuccess = false;
        
        public bool ActByInput(int input, ref GameEnum.eSceneType scene)
        {
            switch (input)
            {
                case 1:
                    IOStream.SavePlayerJsonFile();
                    _isSaveLoadSuccess = true;
                    return true;
                case 2:
                    Character.Player = IOStream.LoadPlayerJsonFile();
                    _isSaveLoadSuccess = true;
                    return true;
                case 3:
                    GameManager.UIManager.ConsoleClear();
                    DisplayRoute();
                    var routes = GameManager.SceneManager.GetEnableScene(_type);

                    int inputNum = int.Parse(Console.ReadLine());
                    if (routes.Length < inputNum || inputNum < 0)
                        return false;

                    GameManager.SceneManager.ChangeScene(ref scene, routes[inputNum]);

                    return true;
            }

            return false;
        }

        public void MainLoop()
        {
            GameManager.UIManager.ConsoleClear();
            DisplaySaveLoadScene();
        }


        private void DisplayRoute()
        {
            GameManager.UIManager.ConsoleClear();
            StringBuilder sb = new StringBuilder();
            Define.GameEnum.eSceneType[] routes = GameManager.SceneManager.GetEnableScene(_type);
            sb.AppendLine("다른 씬으로 이동");

            for (int i = 0; i < routes.Length; i++) 
            {
                switch (routes[i])
                {
                    case Define.GameEnum.eSceneType.Town:
                        sb.Append($"{i}. 마을\n");
                        break;
                    default:
                        sb.Append("오류입니다. 수정하세요.\n");
                        break;
                }
            }
            sb.Append("\r\n" +
                          "원하는 행동을 입력해주세요.\r\n" +
                          ">> ");
            MessageToUIManager(sb.ToString());

        }

        private void DisplaySaveLoadScene()
        {
            StringBuilder sb = new StringBuilder();
            if (_isSaveLoadSuccess)
            {
                sb.Append("저장/불러오기가 완료되었습니다.\n");
                _isSaveLoadSuccess = false;
            }
            else
            {
                sb.Append("저장 및 불러오기\n");
            }
            sb.Append( "\r\n" +
                            "1. 저장하기\r\n" +
                            "2. 불러오기\r\n" +
                            "3. 다른 씬으로 이동\r\n" +
                            "\r\n" +
                            "원하시는 행동을 입력하세요.\r\n" +
                            ">> ");

            MessageToUIManager(sb.ToString());
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
