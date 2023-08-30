using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Manager;
using TextRPGing.Model;
using TextRPGing.Utils;

namespace TextRPGing.Scene
{
    public class Town : Define.Interface.IScene
    {
        public bool ActByInput(int input)
        {
            var Routes = GameManager.SceneManager.GetEnableScene();
            if (Routes.Length >= input)
                return false;
            else
            {
                GameManager.SceneManager.ChangeScene(Routes[input]);
                return true;
            }
        }

        public void MainLoop()
        {
            StringBuilder sb = new StringBuilder();
            List<string> sbs = new List<string>();
            if (Character.Player == null)
            {
                MessageToUI messageToUI;
                // 타이틀 출력
                sb.Clear();
                sb.Append($"내일배움캠프에 당도한 것을 환영하오, 낯선이여.\n");
                sb.Append($"나는 나의 훌륭한 학생들을 굽어살피는 깨우친 튜터, 염예찬이오.\n\n");
                sbs.Add(sb.ToString());
                
                // 이름 선택 출력
                sb.Clear();
                sb.Append($"이름을 말하시오 : ");
                sbs.Add(sb.ToString());
                messageToUI = new MessageToUI(Define.GameEnum.eSceneType.Town, sbs.ToArray());
                GameManager.UIManager.PutToOutQueue(messageToUI);
                GameManager.UIManager.DisplayUpdate();
                sbs.Clear();

                // input 확인
                var name = CheckInput();

                // 직업 선택 출력
                sb.Clear();
                sb.Append($"직업이 무엇이오?\n");
                string[] jobs = { "전사", "도적", "궁수", "마법사" };
                for (int i = 0; i <= (int)Define.GameEnum.eCharacterClass.Magician; ++i)
                {
                    sb.Append($"{i}. {jobs[i]}\n");
                }
                sbs.Add(sb.ToString());
                messageToUI = new MessageToUI(Define.GameEnum.eSceneType.Town, sbs.ToArray());
                GameManager.UIManager.PutToOutQueue(messageToUI);
                GameManager.UIManager.DisplayUpdate();
                sbs.Clear();

                // input 확인
                var job = CheckInput();

                Character.Player = new Character();
                Character.Player.Name = name;
            }
            else
            {
                // 타이틀 출력
                sb.Clear();
                sb.Append($"");
                sbs.Add(sb.ToString());
                // 이벤트 출력
                sb.Clear();
                sb.Append($"");
                sbs.Add(sb.ToString());
                // 행선지 출력
                sb.Clear();
                sb.Append($"");
                sbs.Add(sb.ToString());
            }
            MessageToUI message = new MessageToUI(Define.GameEnum.eSceneType.Town, sbs.ToArray());
            GameManager.UIManager.PutToOutQueue(message);
            GameManager.UIManager.DisplayUpdate();
        }
        private string CheckInput()
        {
            string input = null;
            while (input == null)
            {
                input = Console.ReadLine();
                if (input.Length < 0)
                {
                    string[] what = new string[] { "뭐라 하셨소?\n" };
                    MessageToUI messageToUI = new MessageToUI(Define.GameEnum.eSceneType.Town, what);
                    GameManager.UIManager.PutToOutQueue(messageToUI);
                    GameManager.UIManager.DisplayUpdate();
                    input = null;
                }
            }
            return input;
        }
    }


}
