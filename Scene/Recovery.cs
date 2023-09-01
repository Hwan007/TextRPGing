using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;
using TextRPGing.Manager;
using TextRPGing.Model;
using TextRPGing.Utils;

namespace TextRPGing.Scene
{
    public class Recovery : IScene
    {
        Define.GameEnum.eSceneType type = Define.GameEnum.eSceneType.Recovery;
        private int tempHP;
        private int count = 0;
        int index = 0;

        private enum HealingState
        {
            isHeal,
            isOut
        }
        private int heal = -1;
        HealingState healingState = HealingState.isHeal;
        public void MainLoop()
        {
            GameManager.UIManager.ConsoleClear();
            DisplayRecoveryScene();
            DisplayHeal(heal);
            DisplayRoute();

        }
        public bool ActByInput(int input, ref Define.GameEnum.eSceneType scene)
        {
            if (input == index && healingState == HealingState.isHeal && count > 0)
            {
                tempHP = Character.Player.HP;
                Character.Player.TakeHeal(30);
                heal = Character.Player.HP - tempHP;
                if (heal > 0)
                {
                    foreach (Item item in Character.Player.Inven.Items)
                    {
                        if (item.Type == Define.GameEnum.eItemType.Potion)
                        {
                            Character.Player.Inven.Items.Remove(item);
                            break;
                        }
                    }
                }
                return true;
            }
            else if (input == index && healingState == HealingState.isHeal && count == 0)
            {
                heal = -2;
                return true;
            }
            else if (input >= 0 && input < index)
            {
                var Routes = GameManager.SceneManager.GetEnableScene(scene);
                if (Routes.Length <= input || input < 0)
                    return false;
                else
                {
                    scene = Routes[input];
                    healingState = HealingState.isHeal;
                    heal = -1;
                    return true;
                }
            }
            return false;
            
        }
            
        


        private void DisplayRecoveryScene()
        {
            GameManager.UIManager.ConsoleClear();
            string message = "회복\n아이템을 사용해 체력을 회복할 수 있습니다.\r\n";

            MessageToUIManager(message);
        }
        
        private void DisplayHeal(int heal)
        {
            count = 0;
            string message = "";
            foreach (Item item in Character.Player.Inven.Items)
            {
                if (item.Type == Define.GameEnum.eItemType.Potion)
                {
                    count++;
                }                
            }

            if (heal > 0)
                message += $"체력을 +{heal} 회복하였습니다.\r\n";
            else if (heal == 0) message += "이미 체력이 가득 찼습니다.\r\n";
            else if (heal == -2) message += "포션이 없습니다...\r\n";
            MessageToUIManager(message);
        }

        private void DisplayRoute()
        {
            index = 0;
            Define.GameEnum.eSceneType[] routeList = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Recovery);
            string message = $"소지개수 : {count}\n\n체력: {Character.Player.HP}/{Character.Player.MaxHP}\n\n\n\n\n";

            foreach (var route in routeList)
            {
                //message += $"{index}. " + routeList[index] + "\n";
                switch (route)
                {
                    case Define.GameEnum.eSceneType.Town:
                        message += $"{index}. 나가기\n";
                        break;
                    case Define.GameEnum.eSceneType.Status:
                        message += $"{index}. 상태보기\n";
                        break;
                    case Define.GameEnum.eSceneType.Inventory:
                        message += $"{index}. 인벤토리\n";
                        break;
                    case Define.GameEnum.eSceneType.Battle:
                        message += $"{index}. 전투 시작\n";
                        break;
                    case Define.GameEnum.eSceneType.Store:
                        message += $"{index}. 상점\n";
                        break;
                    case Define.GameEnum.eSceneType.SaveLoad:
                        message += $"{index}. 저장하기/불러오기\n";
                        break;
                    default:
                        message += $"{index}. 오류입니다. 수정하세요.\n";
                        break;
                }
                index++;
            }
            message += $"{index}. 회복\r\n" +
                       "원하는 행동을 입력해주세요.\r\n" +
                       ">> ";
            MessageToUIManager(message);

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
