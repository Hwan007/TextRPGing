using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
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

        private enum OrderMethod
        {
            name,
            atk,
            def
        }

        InvenState state = InvenState.basic;
        OrderMethod method = OrderMethod.name;

        int index = 0;

        bool isChanging = false;
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
                case InvenState.changeOrder:
                    ChangeOrder(items);
                    DisplayRoute();
                    break;
            }
            
        }

        public bool ActByInput(int input, ref Define.GameEnum.eSceneType scene)
        {
            if (input == index - 1 && state == InvenState.basic)
            {
                state = InvenState.changeItem;
                return true;
            }
            else if (input >= 0 && input < Character.Player.Inven.Items.Count && state == InvenState.changeItem)
            {
                if (Character.Player.Inven.Items[input].Type != Define.GameEnum.eItemType.Potion)
                {
                    Character.Player.Equip.EquipItem(Character.Player.Inven.Items[input]);
                    return true;
                }
                else
                {
                    Console.WriteLine("세상에 포션을 싸울 때 쓰는 사람이 어디 있을까? 물건은 용도에 맞게 쓰자.");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            else if (input == index && state == InvenState.basic)
            {
                state = InvenState.changeOrder;
                return true;
            }
            else if (input > 0 && input <= index && state == InvenState.changeOrder && isChanging == false)
            {
                method = (OrderMethod)(input-1);
                isChanging = true;
                return true;
            }
            else if (input == 1 && isChanging == true && state == InvenState.changeOrder || input == 2 && isChanging == true && state == InvenState.changeOrder)
            {
                switch (method)
                {
                    case OrderMethod.name:
                        if (input == 1)
                        {
                            Character.Player.Inven.Items = Character.Player.Inven.Items.OrderBy(item => item.Name.Length).ToList();
                        }
                        else
                        {
                            Character.Player.Inven.Items = Character.Player.Inven.Items.OrderByDescending(item => item.Name.Length).ToList();
                        }
                        isChanging = false;
                        state = InvenState.basic;
                        break;
                    case OrderMethod.atk:
                        if (input == 1)
                        {
                            Character.Player.Inven.Items = Character.Player.Inven.Items.OrderBy(item => (int)item.Type).ToList();
                            //int itemNum = 0;

                        }
                        else
                        {
                            Character.Player.Inven.Items = Character.Player.Inven.Items.OrderByDescending(item => (int)item.Type).ToList();
                        }
                        isChanging = false;
                        state = InvenState.basic;
                        break;
                }
                return true;
            }
            
            else if (input == Character.Player.Inven.Items.Count && state == InvenState.changeItem)
            {
                state = InvenState.basic;
                return true;
            }
            else if (input >= 0 && state == InvenState.basic)
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
                message += PrintItemInfo(item);

            }

            MessageToUIManager(message);
        }

        public void ChangeItems(Item[] items)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
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
                    foreach (Item eqItem in Character.Player.Equip.Items)
                    {
                        if (eqItem.Name == item.Name)
                        {
                            message = message.Substring(0, message.Length - 3);
                            message += "[E]";
                        }
                    }
                    message += $"{item.Name}";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                message += "|";
                message += PrintItemInfo(item);
            }

            MessageToUIManager(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ChangeOrder(Item[] items)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
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
                message += PrintItemInfo(item);

            }

            MessageToUIManager(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private string PrintItemInfo(Item item)
        {
            int nameLength;
            byte[] data;
            int blank;
            string message = "";
            if (item == null) return message; 
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
        
            return message;
        }

        private void DisplayRoute()
        {
            string message = "";
            Define.GameEnum.eSceneType[] routeList = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Inventory);
            if (state == InvenState.basic)
            {
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
                            $"{++index}. 아이템 정렬\r\n";
            }
            else if (state == InvenState.changeOrder && isChanging == false)
            {
                message += $"{++index}. 이름\r\n{++index}. 아이템 종류\r\n";
            }
            else if (state == InvenState.changeOrder && isChanging == true)
            {
                message += $"{++index}. 오름차순\r\n{++index}. 내림차순\r\n";
            }


            else message += $"{index}. 종료\r\n";
            message += $"" +
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
