using System;
using System.Collections.Generic;
using System.Drawing;
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
        private enum InvenState
        {
            basic,
            changeItem,
            changeOrder
        }

        InvenState state = InvenState.basic;

        int index = 0;
        public void MainLoop()
        {
            GameManager.UIManager.ConsoleClear();
            Item[] items = Character.Player.Inven.GetAllItem();

            switch (state)
            {
                case InvenState.basic:
                    DisplayItems(items);
                    DisplayRoute();
                    break;
                case InvenState.changeItem:
                    ChangeItems(items);
                    DisplayRoute();
                    break;
            }
            
        }

        public bool ActByInput(int input, ref Define.GameEnum.eSceneType scene)
        {
            if (input == index && state == InvenState.basic)
            {
                state = InvenState.changeItem;
                return true;
            }
            else if (input >= 0 && input < Character.Player.Inven.Items.Count)
            {
                Character.Player.Equip.EquipItem(Character.Player.Inven.Items[input]);
            }
            else if (input >= 0 && input < index && state == InvenState.basic)
            {
                var Routes = GameManager.SceneManager.GetEnableScene(scene);
                if (Routes.Length <= input || input < 0)
                    return false;
                else
                {
                    scene = Routes[input];
                    state = InvenState.basic;
                    return true;
                }
            }
            return false;
        }

        public void DisplayItems(Item[] items)
        {
            index = 0;
            int nameLength;
            byte[] data;
            int blank;
            string message = "인벤토리\n캐릭터의 아이템을 관리합니다.\n\n";
           
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
                message += "|";
                switch(item.Type)
                {
                    case Define.GameEnum.eItemType.Weapon:
                        data = Encoding.Unicode.GetBytes($"ATK +{((Weapon)item).ATK}");
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
                            message += $"ATK +{((Weapon)item).ATK}  CRT +{((Weapon)item).CRT}";
                        }
                        break;
                    case Define.GameEnum.eItemType.Armor:
                        data = Encoding.Unicode.GetBytes($"ATK +{((Armor)item).DEF}");
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
                            message += $"DEF +{((Armor)item).DEF}  AVD +{((Armor)item).AVD}";
                        }
                        break;
                    case Define.GameEnum.eItemType.Potion:
                        data = Encoding.Unicode.GetBytes($"회복량  30");
                        nameLength = data.Length;
                        blank = (38 - nameLength) / 2;

                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < blank; j++)
                            {
                                message += " ";
                            }
                            if (i == 1)
                                break;
                            message += $"회복량 +30";
                        }
                        break;

                }
                
                message += $"|\t{item.Description}\n";
                }

            MessageToUIManager(message);
        }

        public void ChangeItems(Item[] items)
        {
            int nameLength;
            byte[] data;
            int blank;
            string message = "인벤토리\n캐릭터의 아이템을 관리합니다.\n\n";
            index = 0;
            foreach (Item item in items)
            {
                message += $"{index}.";
                index++;
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
                message += "|";
                switch (item.Type)
                {
                    case Define.GameEnum.eItemType.Weapon:
                        data = Encoding.Unicode.GetBytes($"ATK +{((Weapon)item).ATK}");
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
                            message += $"ATK +{((Weapon)item).ATK}  CRT +{((Weapon)item).CRT}";
                        }
                        break;
                    case Define.GameEnum.eItemType.Armor:
                        data = Encoding.Unicode.GetBytes($"ATK +{((Armor)item).DEF}");
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
                            message += $"DEF +{((Armor)item).DEF}  AVD +{((Armor)item).AVD}";
                        }
                        break;
                    case Define.GameEnum.eItemType.Potion:
                        data = Encoding.Unicode.GetBytes($"회복량  30");
                        nameLength = data.Length;
                        blank = (38 - nameLength) / 2;

                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < blank; j++)
                            {
                                message += " ";
                            }
                            if (i == 1)
                                break;
                            message += $"회복량 +30";
                        }
                        break;

                }

                message += $"|\t{item.Description}\n";
            }

            MessageToUIManager(message);
        }

        private void DisplayRoute()
        {
            string message = "";
            Define.GameEnum.eSceneType[] routeList = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Inventory);
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
                    default:
                        message += $"{index}. 오류입니다. 수정하세요.\n";
                        break;
                }
                index++;
            }
            message += $"{index}. 아이템 관리\r\n" +
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
