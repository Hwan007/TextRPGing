using System;
using System.Collections.Generic;
using System.Data;
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
    public class Battle : IScene
    {
        UIManager uIManager = new UIManager();

        Define.GameEnum.eSceneType Type = Define.GameEnum.eSceneType.Battle;

        private enum StateType
        {
            ChooseAction,
            ChooseMonster,
            PlayerAttackResult,
            MonsterAttackResult,
            Result
        }

        private StateType CurrentState;

        public Character player;
        private Stage mStage;
        private IAction[] mActions = new IAction[3];

        public void MainLoop()
        {
            mActions[0] = new Attack(10);
            uIManager.ConsoleClear();
            if (mStage == null)
            {
                mStage = new Stage();
                CurrentState = StateType.ChooseAction;
            }
            switch (CurrentState)
            {
                case StateType.ChooseAction:
                    DisplayBattleScene();
                    break;
                case StateType.ChooseMonster:
                    DisplayBattleScene();
                    break;
                case StateType.PlayerAttackResult:
                    DisplayBattleScene();
                    break;
                case StateType.MonsterAttackResult:
                    DisplayBattleScene();
                    break;
            }
        }

        public bool ActByInput(int Input)
        {
            return false; // 임시로 써놓은겁니다.
        }

        public void DisplayBattleScene()
        {
            string battle = "Battle!\n\n";

            string monsterMob = "";
            switch (CurrentState)
            {
                case StateType.ChooseAction:
                    foreach (var monster in mStage.Monsters)
                    {
                        monsterMob += $"{monster.Name}\n{monster.HP}/{monster.MaxHP}\n\n";
                    }
                    break;
                case StateType.ChooseMonster:
                    int monsterIndex = 0;
                    foreach (var monster in mStage.Monsters)
                    {
                        monsterMob += $"{monsterIndex}. {monster.Name}\n{monster.HP}/{monster.MaxHP}\n\n";
                        monsterIndex++;
                    }
                    break;
            }
            string playerStatus = $"[내 정보]\n Lv.{player.Level} {player.Name} [플레이어 직업]\nHP {player.HP}/{player.MaxHP}\n\n";

            switch (CurrentState)
            {
                case StateType.ChooseAction:
                    battle += monsterMob + "\"{monstersMob}{playerStatus}1. 일반 공격\n2. 스킬\n\n원하시는 행동을 입력하세요\n>> \"";
                    break;
                case StateType.ChooseMonster:
                    battle += monsterMob + "\n\n\n대상의 번호를 입력하세요. 0 입력 시 행동 선택으로 돌아갑니다\n>>";
                    break;
                case StateType.PlayerAttackResult:
                    battle += $"{player.Name}의 공격! ";
                    break;
                case StateType.MonsterAttackResult:
                    battle += $"{mStage.Monsters[1].Name}의 공격! ";
                    break;
            }


        }

        private void MessageToUIManager(string message)
        {
            MessageToUI messageToUI = new MessageToUI(GameEnum.eSceneType.Battle, new string[] { message});

            uIManager.PutToOutQueue(messageToUI);
            uIManager.DisplayUpdate();
            uIManager.ClearMessageQueue();
        }
    }
}
