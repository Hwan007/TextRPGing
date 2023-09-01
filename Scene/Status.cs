using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Manager;
using TextRPGing.Model;
using TextRPGing.Utils;

namespace TextRPGing.Scene
{
    public class Status : Define.Interface.IScene
    {
        private string[] mJobName = new string[] { "전사", "도적", "궁수", "마법사" };
        public bool ActByInput(int input, ref Define.GameEnum.eSceneType scene)
        {
            var Routes = GameManager.SceneManager.GetEnableScene(scene);
            if (Routes.Length <= input)
                return false;
            else
            {
                GameManager.SceneManager.ChangeScene(ref scene, Routes[input]);
                return true;
            }
        }

        public void MainLoop()
        {
            GameManager.UIManager.ConsoleClear();
            List<string> sbs = new List<string>();
            StringBuilder sb = new StringBuilder();
            // 타이틀 출력
            sb.Clear();
            sb.Append("상태보기\n");
            sb.Append("캐릭터의 정보가 표시됩니다.\n\n");
            sbs.Add(sb.ToString());

            // 캐릭터 정보 출력
            sb.Clear();
            sb.Append($"LV. {Character.Player.Level}\n");
            sb.Append($"{Character.Player.Name}  ({mJobName[(int)Character.Player.Job]})\n");
            sb.Append($"공격력 : {Character.Player.ATK}\n");
            sb.Append($"방어력 : {Character.Player.DEF}\n");
            sb.Append($"체  력 : {Character.Player.HP}/{Character.Player.MaxHP}\n");
            sb.Append($"회피율 : {Character.Player.AVD}\n");
            sb.Append($"치 확 : {Character.Player.CRT}\n");
            sb.Append($"골  드 : {Character.Player.Inven.Gold}\n\n");
            sb.Append($"장  비\n");
            sb.Append(GetItemList(Character.Player.Equip.Items));
            sbs.Add(sb.ToString());
            
            // 선택지 출력
            sb.Clear();
            var Routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Status);
            int i = 0;
            foreach (var route in Routes)
            {
                switch (route)
                {
                    case Define.GameEnum.eSceneType.Town:
                        sb.Append($"{i}. 나가기\n");
                        break;
                    case Define.GameEnum.eSceneType.Battle:
                        sb.Append($"{i}. 전투 시작\n");
                        break;
                    case Define.GameEnum.eSceneType.Store:
                        sb.Append($"{i}. 상점\n");
                        break;
                    case Define.GameEnum.eSceneType.Status:
                        sb.Append($"{i}. 상태보기\n");
                        break;
                    case Define.GameEnum.eSceneType.Recovery:
                        sb.Append($"{i}. 포션 사용\n");
                        break;
                    case Define.GameEnum.eSceneType.Inventory:
                        sb.Append($"{i}. 인벤토리\n");
                        break;
                    case Define.GameEnum.eSceneType.SaveLoad:
                        sb.Append($"{i}. 저장하기/불러오기\n");
                        break;
                    default:
                        sb.Append($"{i}. 오류입니다. 수정해주세요.\n");
                        break;
                }
                ++i;
            }
            sbs.Add(sb.ToString());

            MessageToUI message = new MessageToUI(Define.GameEnum.eSceneType.Status, sbs.ToArray());
            GameManager.UIManager.PutToOutQueue(message);
            GameManager.UIManager.DisplayUpdate();
            GameManager.UIManager.ClearMessageQueue();
        }

        private string GetItemList(List<Item> itmes)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder tempStr = new StringBuilder();
            var routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Store);
            for (int i = routes.Length; i < itmes.Count + routes.Length; ++i)
            {
                int index = i - routes.Length;
                tempStr.Clear();
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
                sb.Append(tempStr + "\n");
            }
            sb.Append("\n\n");
            return sb.ToString();
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
    }
}
