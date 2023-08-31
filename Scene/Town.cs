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
        private string[] jobs = { "전사", "도적", "궁수", "마법사" };
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
            
            if (Character.Player == null)
            {
                // 타이틀 출력
                SendToUIManager($"내일배움캠프에 당도한 것을 환영하오, 낯선이여.\n");
                SendToUIManager($"나는 나의 훌륭한 학생들을 굽어살피는 깨우친 튜터, 염예찬이오.\n\n");
                
                // 이름 선택 출력
                SendToUIManager($"이름을 말하시오.\n\n");
                GameManager.UIManager.DisplayUpdate();

                // input 확인
                var name = CheckName();

                // 직업 선택 출력
                SendToUIManager($"직업이 무엇이오?\n");
                for (int i = 0; i <= (int)Define.GameEnum.eCharacterClass.Magician; ++i)
                {
                    SendToUIManager($"{jobs[i]}\t");
                }
                SendToUIManager("\n\n");
                GameManager.UIManager.DisplayUpdate();

                // input 확인
                var job = CheckJob();

                if (name != null && job.HasValue)
                    Character.Player = new Character(name, job.Value);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                List<string> sbs = new List<string>();

                // 타이틀 출력
                sb.Clear();
                sb.Append($"내일배움캠프\n");
                sb.Append($"평화로운 캠프로 보이지만, 그 뒤에는 튜터분들과 매니저분들의 노력이 있다.\n\n");
                sbs.Add(sb.ToString());

                // 이벤트 출력
                //sb.Clear();
                //sb.Append($"");
                //sbs.Add(sb.ToString());

                // 행선지 출력
                sb.Clear();
                AppendRoutes(ref sb);
                sbs.Add(sb.ToString());

                SendToUIManager(sbs.ToArray());
                GameManager.UIManager.DisplayUpdate();
            }
        }
        private void AppendRoutes(ref StringBuilder sb)
        {
            var routes = GameManager.SceneManager.GetEnableScene();
            for (int i = 0; i<routes.Length; ++i)
            {
                sb.Append($"{i}. ");
                switch(routes[i])
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
                        sb.Append($"저장하기/불러오기\n");
                        break;
                    default:
                        sb.Append($"오류입니다. 수정해주세요.\n");
                        break;
                }
            }
            sb.Append("\n");
        }
        private string CheckName()
        {
            string input = null;
            while (input == null)
            {
                input = Console.ReadLine();
                if (input.Length < 0)
                {
                    SendToUIManager("뭐라 하셨소?\n");
                    GameManager.UIManager.DisplayUpdate();
                    input = null;
                }
            }
            return input;
        }
        private Define.GameEnum.eCharacterClass? CheckJob()
        {
            string input = null;
            Define.GameEnum.eCharacterClass? job = null;
            while (input == null)
            {
                input = Console.ReadLine();
                if (input.Length < 0)
                {
                    SendToUIManager("뭐라 하셨소?\n");
                    GameManager.UIManager.DisplayUpdate();
                    input = null;
                }
                else if (jobs.Contains(input))
                {
                    if (input == jobs[0])
                        job = Define.GameEnum.eCharacterClass.Warrior;
                    else if (input == jobs[1])
                        job = Define.GameEnum.eCharacterClass.Thief;
                    else if (input == jobs[2])
                        job = Define.GameEnum.eCharacterClass.Archer;
                    else if (input == jobs[3])
                        job = Define.GameEnum.eCharacterClass.Magician;
                }
                else if (int.TryParse(input, out int ret))
                {
                    if (ret >= 0 && ret < 4)
                    {
                        switch (ret)
                        {
                            case 0:
                                job = Define.GameEnum.eCharacterClass.Warrior;
                                break;
                            case 1:
                                job = Define.GameEnum.eCharacterClass.Thief;
                                break;
                            case 2:
                                job = Define.GameEnum.eCharacterClass.Archer;
                                break;
                            case 3:
                                job = Define.GameEnum.eCharacterClass.Magician;
                                break;
                        }
                    }
                    else
                    {
                        SendToUIManager("그런 직업은 없소이다.\n");
                        GameManager.UIManager.DisplayUpdate();
                        input = null;
                    }
                }
            }
            return job;
        }
        private void SendToUIManager(string message)
        {
            string[] messageArr = new string[] { message };
            MessageToUI messageToUI = new MessageToUI(Define.GameEnum.eSceneType.Town, messageArr);
            GameManager.UIManager.PutToOutQueue(messageToUI);
        }
        private void SendToUIManager(string[] message)
        {
            MessageToUI messageToUI = new MessageToUI(Define.GameEnum.eSceneType.Town, message);
            GameManager.UIManager.PutToOutQueue(messageToUI);
        }
    }


}
