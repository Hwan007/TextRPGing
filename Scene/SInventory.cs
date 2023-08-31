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
    public class SInventory : IScene
    {
        private enum invenState
        {
            basic,
            changeItem,
            changeOrder
        }
        
        public void MainLoop()
        {
            GameManager.UIManager.ConsoleClear();
            Item[] items = Character.Player.Inven.GetAllItem();
            DisplayItems(items);
        }

        public bool ActByInput(int input, ref Define.GameEnum.eSceneType scene)
        {
            return false;
        }

        public void DisplayItems(Item[] items)
        {
            int nameLength;
            byte[] data;
            int blank;
            string message = "인벤토리\n캐릭터의 아이템을 관리합니다.\n";
           
            foreach (Item item in items)
            {
                    string itemName = item.Name;
                    data = Encoding.Unicode.GetBytes(itemName);
                    nameLength = data.Length;
                    blank = (30 - nameLength) / 2;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < blank; j++)
                        {
                            message += " ";
                        }
                        if (i == 1)
                            break;
                        message += $"{item.Name}";
                    }

                    //data = Encoding.Unicode.GetBytes($"ATK +{item.ATK}");
                    //nameLength = data.Length;
                    //blank = (30 - nameLength) / 2;

                    //for (int i = 0; i < 2; i++)
                    //{
                    //    for (int j = 0; j < blank; j++)
                    //    {
                    //        message += " ";
                    //    }
                    //    if (i == 1)
                    //        break;
                    //    message += $"ATK +{item.ATK}";
                    //}
                    message += $"|\t{item.Description}\n";
                }

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
