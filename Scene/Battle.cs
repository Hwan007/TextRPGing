using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
            BattleResult
        }

        private StateType CurrentState;

        private Stage mStage;
        private IAction[] mActions = new IAction[2];
        public bool isPlayerTurn = true;
        Random random = new Random();
        private int monsterAttack = 0;
        public void MainLoop()
        {
            mActions[0] = new Attack(Character.Player.ATK);
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
                    isPlayerTurn = false;
                    DisplayBattleScene();
                    break;
                case StateType.MonsterAttackResult:
                    DisplayBattleScene();
                    break;
                case StateType.BattleResult:
                    DisplayBattleScene();
                    break;
            }
        }

        public bool ActByInput(int input, ref Define.GameEnum.eSceneType scene)
        {
            // CurrentState에 따라서 행동을 해야한다. => switch를 써야 보기 좋다.
            switch (CurrentState)
            {
                case StateType.ChooseAction:
                    return BattleActionMethod(input, ref scene);
                case StateType.ChooseMonster:
                    return BattleActionMethod(input, ref scene);
            }
            // 1. StateType.ChooseAction에서는 행동 리스트를 표시했으니, 인풋에 따라서 행동을 선택해야 한다. ※ 필요한 데이터 = 행동 리스트
            // => CurrentState를 ChooseMonster로 바꾼다.ㅊ

            // 2. StateType.ChooseMonster에서는 몬스터 리스트를 표시했으니, 인풋에 따라서 몬스터를 선택해야 한다. ※ 필요한 데이터 = 몬스터 리스트
            // => 3번을 표시하기 전(즉, ChooseMonster에서 PlayerAttackResult에서는 넘어가기 전)에 PlayerAttackResult에 표시할 AttackResult를 생성해야 합니다. ※ 필요한 데이터 = 선택한 행동 index, 선택한 몬스터 index
            // => CurrentState를 StateType.PlayerAttackResult로 바꾼다.ㅊ    

            // => 혹은 행동과 몬스터를 가지고 PlayerAttackResult에서 결과값을 가져오고 바로 출력하는 것도 나쁘지 않다.
            // 3. StateType.PlayerAttackResult에서는 선택한 행동을 해당 몬스터에 대한 결과값을 표시해야한다. ※ 필요한 데이터 = AttackResult
            // => 2번과 마찬가지로 결과를 표시하기전에 결과값을 가지고 있는 것이 좋다.
            // => 참고로 죽은 몬스터를 공격하면 안된다는 것을 유의해야 한다.
            // => CurrentState를 StateType.MonsterAttackResult로 바꾼다.ㅊ

            // 4. StateType.MonsterAttackResult에서는 몬스터가 자동으로 나를 공격한 결과를 몬스터 수에 따라서 표시해야한다. ※ 필요한 데이터 = AttackResult[공격한 몬스터 수]
            // => 한번의 루프가 돌았으니 모든 몬스터 혹은 플래이어가 사망하였는지 체크하는 것도 나쁘지 않다.
            // => 이제 다시 1번으로 돌아가야 하므로 CurrentState를 StateType.ChooseAction으로 바꾼다.ㅊ
            return false;
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
                        monsterMob += $"{monster.Name} {monster.HP}/{monster.MaxHP}\n";
                    }
                    break;
                case StateType.ChooseMonster:
                    int monsterIndex = 1;
                    foreach (var monster in mStage.Monsters)
                    {
                        monsterMob += $"{monsterIndex} {monster.Name} {monster.HP}/{monster.MaxHP}\n";
                        monsterIndex++;
                    }
                    break;
            }
            string playerStatus = $"\n[내 정보]\nLv.{Character.Player.Level} {Character.Player.Name} {Character.Player.Job}\nHP {Character.Player.HP}/{Character.Player.MaxHP}\n\n";
            monsterAttack = random.Next(0, mStage.Monsters.Length);
            switch (CurrentState)
            {
                case StateType.ChooseAction:
                    battle += monsterMob + playerStatus + $"1. 일반 공격\n2. {Character.Player.Skill}\n\n원하시는 행동을 입력하세요\n>> ";
                    break;
                case StateType.ChooseMonster:
                    battle += monsterMob + playerStatus + "\n\n\n대상의 번호를 입력하세요.\n>>";
                    break;
                case StateType.PlayerAttackResult:
                    battle += $"{Character.Player.Name}의 공격 결과: \n\n";
                    battle += $"{Character.Player.Name}은 {targetMonster.Name}에게 {damage}의 데미지를 입혔습니다!\n\n";
                    if (targetMonster.HP <= 0)
                    {
                        battle += $"{targetMonster}는 HP가 0이 되어 쓰러졌습니다!\n\n";
                    }
                    break;
                case StateType.MonsterAttackResult:
                    if (mStage.Monsters.Length > 0)
                    {
                        battle += $"{mStage.Monsters[monsterAttack]}은 {Character.Player.Name}에게 {damage}의 데미지를 입혔습니다!";
                    }
                    else
                    {
                        CurrentState = StateType.BattleResult;
                    }
                    break;
                case StateType.BattleResult:
                    battle += "모든 몬스터를 쓰러뜨렸습니다!";
                    break;
            }
            MessageToUIManager(battle);
        }

        IAction chosenAction;
        public Monster targetMonster = null;
        private int damage = 0;
        private bool BattleActionMethod(int input, ref Define.GameEnum.eSceneType scene)
        {
            switch (CurrentState)
            {
                case StateType.ChooseAction:
                    if (input >= 1 && input <= 2)
                    {
                        chosenAction = mActions[input - 1];
                        CurrentState = StateType.ChooseMonster;
                        return true;
                    }
                    else
                    {
                        break;
                    }
                case StateType.ChooseMonster:
                    if (input >= 1 && input <= mStage.Monsters.Length && targetMonster == null)
                    {
                        targetMonster = mStage.Monsters[input - 1];
                        damage = mActions[0].GetDamage(Character.Player.ATK, Character.Player.DEF, Character.Player.CRT, Character.Player.AVD);
                        if (isPlayerTurn == true)
                        {
                            targetMonster.TakeDamage(damage);
                        }
                        CurrentState = StateType.PlayerAttackResult;
                        return true;
                    }
                    else
                    {
                        break;
                    }
            }
            return false;
        }

        private void MessageToUIManager(string message)
        {
            MessageToUI messageToUI = new MessageToUI(GameEnum.eSceneType.Battle, new string[] { message });

            uIManager.PutToOutQueue(messageToUI);
            uIManager.DisplayUpdate();
            uIManager.ClearMessageQueue();
        }
    }
}
