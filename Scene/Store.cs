using System;
using System.Collections.Generic;
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
    public class Store : IScene
    {
        public static Store Merchant;
        public List<Item> Items { get; private set; }

        public Store()
        {
            if (Merchant == null)
            {
                Merchant = this;
                Items = new List<Item>();
            }
        }

        public bool ActByInput(int input, ref GameEnum.eSceneType scene)
        {
            var routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Store);
            if (input >= routes.Length + Items.Count)
                return false;
            else
            {
                if (input >= routes.Length)
                    Character.Player.Inven.AddItem(Items[input - routes.Length]);
                else
                {
                    GameManager.SceneManager.ChangeScene(ref scene, routes[input]);
                }
                return true;
            }
        }

        public void MainLoop()
        {
            GameManager.UIManager.ConsoleClear();
            StringBuilder sb = new StringBuilder();
            var routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Store);
            // 제목 출력
            // 상점
            // 아이템을 사고 팔 수 있습니다.
            sb.Append($"상점\n");
            sb.Append($"아이템을 사고 팔 수 있습니다.\n\n");

            // 아이템 리스트 출력
            // 번호 | 이름 | 능력치 | 설명 | 금액
            StringBuilder tempStr = new StringBuilder();
            for (int i = routes.Length; i < Items.Count + routes.Length; ++i)
            {
                // 번호
                if (i > 9)
                    sb.Append($"[{i}]");
                else
                    sb.Append($"[{i}] ");
                // 이름
                tempStr.Append(Items[i].Name);
                tempStr.Append(Fit(tempStr.ToString(), 16));
                sb.Append(tempStr + " | ");
                // 능력치
                tempStr.Clear();
                if (Items[i] is Weapon)
                {
                    var item = Items[i] as Weapon;
                    tempStr.Append("공격력 +" + item.ATK);
                }
                else if (Items[i] is Armor)
                {
                    var item = Items[i] as Armor;
                    tempStr.Append("방어력 +" + item.DEF);
                }
                else if (Items[i] is Potion)
                {
                    var item = Items[i] as Potion;
                    tempStr.Append("회복력 +" + item.Heal);
                }
                tempStr.Append(Fit(tempStr.ToString(), 12));
                sb.Append(tempStr + " | ");
                // 설명
                tempStr.Clear();
                tempStr.Append($"{Items[i].Description}");
                tempStr.Append(Fit(tempStr.ToString(), 40));
                sb.Append(tempStr + " | ");
                // 금액
                sb.Append($"{Items[i].Price}" + "\n");
            }

            // 행선지 출력
            // 0 나가기
            // 1 판매
            for (int i = 0; i < routes.Length; ++i)
            {
                sb.Append($"{i}. ");
                switch (routes[i])
                {
                    case GameEnum.eSceneType.Town:
                        sb.Append("나가기\n");
                        break;
                    case GameEnum.eSceneType.Inventory:
                        sb.Append("인벤토리\n");
                        break;
                    case GameEnum.eSceneType.Battle:
                        sb.Append("전투 시작\n");
                        break;
                    case GameEnum.eSceneType.Status:
                        sb.Append("상태 보기\n");
                        break;
                    case GameEnum.eSceneType.SaveLoad:
                        sb.Append("저장하기/불러오기\n");
                        break;
                    default:
                        sb.Append("오류입니다 수정해주세요.\n");
                        break;
                }
            }
            MessageAndUpdate(sb.ToString());
        }

        public void AddItemToStore(Item[] itmes)
        {
            Items.AddRange(itmes);
        }
        public void AddItemToStore(Item item)
        {
            Items.Add(item);
        }
        public void ItemListClaer()
        {
            Items.Clear();
        }
        public void RemoveItem(int index)
        {
            Items.RemoveAt(index);
        }
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
        public Item GetItem(int index)
        {
            return Items[index];
        }
        private string Fit(string str, int max)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (char c in str)
            {
                if (c == ' ')
                    ++count;
                else
                    count += 2;
            }
            for (int j = 0; j < max - count; ++j)
            {
                sb.Append(' ');
            }
            return sb.ToString();
        }
        private void MessageAndUpdate(string str)
        {
            string[] strings = new string[] { str };
            MessageToUI message = new MessageToUI(Define.GameEnum.eSceneType.Store, strings);
            GameManager.UIManager.PutToOutQueue(message);
            GameManager.UIManager.DisplayUpdate();
            GameManager.UIManager.ClearMessageQueue();
        }
    }
}
