using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPGing.Manager;
using TextRPGing.Model;
using TextRPGing.Utils;

namespace TextRPGing.Scene
{
    public class Town : Define.Interface.IScene
    {
        private string[] jobs = { "전사", "도적", "궁수", "마법사" };
        private string name;
        private Define.GameEnum.eCharacterClass? job;
        private enum eStateType
        {
            Start,
            LoadCharacter,
            MakeCharacter,
            Main
        }
        private eStateType mState;
        public Town()
        {
            mState = eStateType.Start;
        }
        public bool ActByInput(int input, ref Define.GameEnum.eSceneType scene)
        {
            if (Character.Player == null)
            {
                if (mState == eStateType.Start)
                {
                    if (input == 0)
                        mState = eStateType.LoadCharacter;
                    else if (input == 1)
                        mState = eStateType.MakeCharacter;
                    else
                        return false;
                    return true;
                }
                else if (mState == eStateType.LoadCharacter)
                {
                    if (input == 0)
                        mState = eStateType.Start;
                    else if (input == 1)
                    {
                        Character.Player = IOStream.LoadPlayerJsonFile();
                        SendToUIManager($"불러오기가 완료되었습니다.\n");
                        UpdateUI();
                        Thread.Sleep(1000);
                        mState = eStateType.Main;
                    }
                    else
                        return false;
                    return true;
                }
                else if (mState == eStateType.MakeCharacter)
                {
                    job = CheckJob(input);

                    if (name != null && job.HasValue)
                    {
                        Character.Player = new Character(name, job.Value);
                        Character.Player.Equip = new Equipment();
                        SendToUIManager($"내일배움캠프에 온 것을 환영하오.");
                        UpdateUI();
                        Thread.Sleep(1000);
                        mState = eStateType.Main;
                        return true;
                    }
                }
                return false;
            }
            else
            {
                var Routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Town);
                if (Routes.Length <= input)
                    return false;
                else
                {
                    GameManager.SceneManager.ChangeScene(ref scene, Routes[input]);
                    return true;
                }
            }
        }

        public void MainLoop()
        {
            GameManager.UIManager.ConsoleClear();
            if (Character.Player == null)
            {
                // 불러오기 / 새 캐릭터 만들기
                if (mState == eStateType.Start)
                    StartGameDisplay();
                else if (mState == eStateType.LoadCharacter)
                    LoadCharacterDisplay();
                else if (mState == eStateType.MakeCharacter)
                    MakeNewCharacterDisplay();
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

                // 원하는 행동을 입력해주세요.
                sb.Clear();
                sb.Append("\n원하는 행동을 입력해주세요.\n>>");
                sbs.Add(sb.ToString());

                SendToUIManager(sbs.ToArray());
                UpdateUI();
            }
        }
        private void LoadCharacterDisplay()
        {
            // 타이틀 출력
            SendToUIManager(" ######   ######   ##  ##   ######            #####    #####     #####\n");
            UpdateUI();
            SendToUIManager("   ##     ##       ##  ##     ##              ##  ##   ##  ##   ##\n");
            UpdateUI();
            SendToUIManager("   ##     #####     ####      ##              ##  ##   ##  ##   ##\n");
            UpdateUI();
            SendToUIManager("   ##     ##        ####      ##              #####    #####    ## ###\n");
            UpdateUI();
            SendToUIManager("   ##     ##       ##  ##     ##              ## ##    ##       ##  ##\n");
            UpdateUI();
            SendToUIManager("   ##     ######   ##  ##     ##              ##  ##   ##        #####\n\n\n");
            UpdateUI();

            // 선택지 출력
            // 0 취소
            // 1 캐릭터 불러오기
            SendToUIManager("0. 취소\n");
            SendToUIManager("1. 캐릭터 불러오기\n\n");
            SendToUIManager("원하시는 행동을 입력해주세요.\n>>");
            UpdateUI();
        }
        private void StartGameDisplay()
        {
            // 타이틀 출력
            SendToUIManager(" ######   ######   ##  ##   ######            #####    #####     #####\n");
            UpdateUI();
            Thread.Sleep(100);
            SendToUIManager("   ##     ##       ##  ##     ##              ##  ##   ##  ##   ##\n");
            UpdateUI();
            Thread.Sleep(100);
            SendToUIManager("   ##     #####     ####      ##              ##  ##   ##  ##   ##\n");
            UpdateUI();
            Thread.Sleep(100);
            SendToUIManager("   ##     ##        ####      ##              #####    #####    ## ###\n");
            UpdateUI();
            Thread.Sleep(100);
            SendToUIManager("   ##     ##       ##  ##     ##              ## ##    ##       ##  ##\n");
            UpdateUI();
            Thread.Sleep(100);
            SendToUIManager("   ##     ######   ##  ##     ##              ##  ##   ##        #####\n\n\n");
            UpdateUI();
            Thread.Sleep(100);

            // 선택지 출력
            // 0 캐릭터 생성
            // 1 캐릭터 불러오기
            SendToUIManager("0. 불러오기\n");
            SendToUIManager("1. 새 캐릭터 생성\n\n");
            SendToUIManager("원하시는 행동을 입력해주세요.\n>>");
            UpdateUI();
        }
        private void MakeNewCharacterDisplay()
        {
            // 타이틀 출력
            SendToUIManager($"내일배움캠프에 당도한 것을 환영하오, 낯선이여.\n");
            SendToUIManager($"나는 나의 훌륭한 학생들을 굽어살피는 깨우친 튜터, 염예찬이오.\n\n");

            // 이름 선택 출력
            SendToUIManager($"이름을 말하시오.\n\n>>");
            UpdateUI();

            // input 확인
            name = CheckName();

            // 직업 선택 출력
            SendToUIManager($"직업이 무엇이오?\n");
            for (int i = 0; i <= (int)Define.GameEnum.eCharacterClass.Magician; ++i)
            {
                SendToUIManager($"[{i}] {jobs[i]}\n");
            }
            SendToUIManager("\n\n>>");
            UpdateUI();
        }

        private void AppendRoutes(ref StringBuilder sb)
        {
            var routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Town);
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
                        sb.Append($"회복\n");
                        break;
                    case Define.GameEnum.eSceneType.Inventory:
                        sb.Append($"인벤토리\n");
                        break;
                    case Define.GameEnum.eSceneType.SaveLoad:
                        sb.Append($"저장하기/불러오기\n");
                        break;
                    case Define.GameEnum.eSceneType.Store:
                        sb.Append("상점\n");
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
                    UpdateUI();
                    input = null;
                }
            }
            return input;
        }
        private Define.GameEnum.eCharacterClass? CheckJob(int input)
        {
            Define.GameEnum.eCharacterClass? job = null;
            switch (input)
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
                default:
                    SendToUIManager("그런 직업은 없소이다.\n");
                    UpdateUI();
                    break;
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

        private void UpdateUI()
        {
            GameManager.UIManager.DisplayUpdate();
            GameManager.UIManager.ClearMessageQueue();
        }
    }


}
