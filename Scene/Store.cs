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
        private eState mState;
        private enum eState
        {
            Sell,
            Buy
        }

        public Store()
        {
            if (Merchant == null)
            {
                Merchant = this;
                Items = new List<Item>();
            }
            mState = eState.Buy;
        }

        public bool ActByInput(int input, ref GameEnum.eSceneType scene)
        {
            var routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Store);
            if (input >= routes.Length + Items.Count + 1)
                return false;
            else
            {
                if (input >= routes.Length + 1)
                {
                    if (mState == eState.Buy)
                    {
                        var item = Items[input - routes.Length - 1];
                        Character.Player.Inven.AddItem(item);
                        Character.Player.Inven.Gold -= item.Price;
                    }
                    else
                    {
                        var item = Character.Player.Inven.GetItem(input - routes.Length - 1);
                        Character.Player.Inven.RemoveItem(input - routes.Length - 1);
                        Character.Player.Inven.Gold += item.Price * 85 / 100;
                    }
                }
                else
                {
                    if (input > 1)
                    {
                        GameManager.SceneManager.ChangeScene(ref scene, routes[input - 1]);
                        mState = eState.Buy;
                    }
                    else if (input == 1)
                    {
                        mState = mState == eState.Buy ? eState.Sell : eState.Buy;
                    }
                    else
                    {
                        GameManager.SceneManager.ChangeScene(ref scene, routes[input]);
                        mState = eState.Buy;
                    }
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
            if (mState == eState.Buy)
                sb.Append($"상점\n");
            else
                sb.Append($"상점 - 판매\n");
            sb.Append($"아이템을 사고 팔 수 있습니다.\n\n");

            // StoreItem.json 파일에서 아이템 불러오기
            Items = LoadItemFromStore();

            // 아이템 리스트 출력
            // 번호 | 이름 | 능력치 | 설명 | 금액
            if (mState == eState.Buy)
                sb.Append(GetItemList(Items));
            else
                sb.Append(GetItemList(Character.Player.Inven.Items));

            // 행선지 출력
            // 0 나가기
            // 1 판매
            for (int i = 0; i < routes.Length+1; ++i)
            {
                sb.Append($"{i}. ");
                if (i == 1)
                {
                    if (mState == eState.Sell)
                        sb.Append("구매\n");
                    else
                        sb.Append("판매\n");
                    continue;
                }
                int index = i > 1 ? i - 1 : 0;
                switch (routes[index])
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
                    case GameEnum.eSceneType.Recovery:
                        sb.Append("휴식하기\n");
                        break;
                    default:
                        sb.Append("오류입니다 수정해주세요.\n");
                        break;
                }
            }

            MessageAndUpdate(sb.ToString());
        }

        private List<Item> LoadItemFromStore()
        {
            return IOStream.LoadStoreItem();
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
        private string GetItemList(List<Item> itmes)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder tempStr = new StringBuilder();
            var routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Store);
            for (int i = routes.Length+1; i < itmes.Count + routes.Length+1; ++i)
            {
                int index = i - routes.Length-1;
                tempStr.Clear();
                // 번호
                if (i > 9)
                    sb.Append($"[{i}]");
                else
                    sb.Append($"[{i}] ");
                // 이름
                tempStr.Append(itmes[index].Name);
                tempStr.Append(Fit(tempStr.ToString(), 16));
                sb.Append(tempStr + " | ");
                // 능력치
                tempStr.Clear();
                if (itmes[index] is Weapon)
                {
                    var item = itmes[index] as Weapon;
                    tempStr.Append("공격력 +" + item.ATK);
                }
                else if (itmes[index] is Armor)
                {
                    var item = itmes[index] as Armor;
                    tempStr.Append("방어력 +" + item.DEF);
                }
                else if (itmes[index] is Potion)
                {
                    var item = itmes[index] as Potion;
                    tempStr.Append("회복력 +" + item.Heal);
                }
                tempStr.Append(Fit(tempStr.ToString(), 12));
                sb.Append(tempStr + " | ");
                // 설명
                tempStr.Clear();
                tempStr.Append($"{itmes[index].Description}");
                tempStr.Append(Fit(tempStr.ToString(), 40));
                sb.Append(tempStr + " | ");
                // 금액
                if (mState == eState.Buy)
                    sb.Append($"{itmes[index].Price}\n");
                else
                    sb.Append($"{itmes[index].Price * 85 / 100}\n");
            }
            sb.Append("\n\n");
            return sb.ToString();
        }
    }
}
