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
        public enum HealingState
        {
            noHeal,
            isHeal,
            isFull
        }
        public void MainLoop()
        {
            HealingState healingState = HealingState.noHeal;
            DisplayRecoveryScene();        
            switch(healingState)
            {
                case HealingState.noHeal:
                    DisplayRecoveryScene();
                    break;
                case HealingState.isFull:
                    break;

            }

        }
        public bool ActByInput(int input)
        {
            if (input == 1)
            {
                tempHP = Character.Player.HP;
                Character.Player.TakeHeal(30);
                int healAmount = Character.Player.HP - tempHP;
                if (healAmount > 0)
                {
                    
                }
                return true;
            }
            else if (input == 0)
            {
                GameManager.SceneManager.ChangeScene(Define.GameEnum.eSceneType.Town); return true;
            }
            return false;
        }

        private void DisplayRecoveryScene()
        {
            GameManager.UIManager.ConsoleClear();
            string message = "회복\n아이템을 사용해 체력을 30 회복할 수 있습니다.\r\n";

            MessageToUIManager(message);
        }

        private void DisplayChoice()
        {
            string message = $"소지개수 : [개수]\n\n체력: {Character.Player.HP}/{Character.Player.MaxHP}\n\n\n\n\n" +
                               " ";
                
        }


        private void MessageToUIManager(string message)
        {
            MessageToUI messageToUi = new MessageToUI(
                Define.GameEnum.eSceneType.Town,
                new string[] { message }
            );

            GameManager.UIManager.PutToOutQueue(messageToUi);
            GameManager.UIManager.DisplayUpdate();
            GameManager.UIManager.ClearMessageQueue();
        }

    }
}
