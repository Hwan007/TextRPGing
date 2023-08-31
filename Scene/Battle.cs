using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;
using TextRPGing.Manager;
using TextRPGing.Model;

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
            Console.WriteLine("배틀 시작!");
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
    }
}
