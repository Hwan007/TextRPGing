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
        private enum HealingState
        {
            isHeal,
            isOut
        }
        private int heal = 0;
        HealingState healingState = HealingState.isHeal;
        public void MainLoop()
        {
            GameManager.UIManager.ConsoleClear();
            DisplayRecoveryScene();        
            switch(healingState)
            {
                case HealingState.isHeal:
                    DisplayRecoveryScene();
                    DisplayHeal(heal);
                    break;
                case HealingState.isOut:
                    DisplayRecoveryScene();
                    DisplayRoute();
                    break;

            }

        }
        public bool ActByInput(int input)
        {
            if (input == 1 && healingState == HealingState.isHeal)
            {
                tempHP = Character.Player.HP;
                Character.Player.TakeHeal(30);
                heal = Character.Player.HP - tempHP;
                return true;
            }
            else if (input == 0 && healingState == HealingState.isHeal)
            {
                healingState = HealingState.isOut;
                return true;
            }
            else if (healingState == HealingState.isOut)
            {
                GameManager.SceneManager.ChangeScene((Define.GameEnum.eSceneType)input);
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
            string message = "";
            int count = 0;
            foreach (Item item in Character.Player.Inven.Items)
            {
                if (item.Type == Define.GameEnum.eItemType.Potion)
                {
                    count++;
                }                
            }

            if (heal > 0)
                message += $"체력을 +{heal} 회복하였습니다.\r\n";
            else message += "이미 체력이 가득 찼습니다.\r\n";
            message += $"소지개수 : {count}\n\n체력: {Character.Player.HP}/{Character.Player.MaxHP}\n\n\n\n\n1. 회복\r\n0. 나가기";
            MessageToUIManager(message);
        }

        private void DisplayRoute()
        {
            Define.GameEnum.eSceneType[] routeList = GameManager.SceneManager.GetEnableScene();
            string message = $"소지개수 : [개수]\n\n체력: {Character.Player.HP}/{Character.Player.MaxHP}\n\n\n\n\n";
            int index = 0;
            foreach (var route in routeList)
            {
                message += $"{index}. " + routeList[index] + "\t";
                index++;
            }
            message += "\r\n" +
                       "이동하고 싶은 곳을 입력해주세요.\r\n" +
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
