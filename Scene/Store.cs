using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define;
using TextRPGing.Define.Interface;
using TextRPGing.Manager;
using TextRPGing.Model;

namespace TextRPGing.Scene
{
    public class Store : IScene
    {
        public List<Item> Items { get; private set; }

        public Store()
        {
            Items = new List<Item>();
        }

        public bool ActByInput(int input, ref GameEnum.eSceneType scene)
        {
            var routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Store);

            return false;
        }

        public void MainLoop()
        {
            // 제목 출력
            // 상점
            // 아이템을 사고 팔 수 있습니다.

            // 아이템 리스트 출력
            // 번호 | 이름 | 능력치 | 설명 | 금액

            // 행선지 출력
            // 0 나가기
            // 1 판매
            var routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Store);
        }

        public void ItemToStore(Item[] itmes)
        {
            Items.AddRange(itmes);
        }
        public void ItemToStore(Item item)
        {
            Items.Add(item);
        }
        public void ItemClaer()
        {
            Items.Clear();
        }
        public void ItemRemove(int index)
        {
            Items.RemoveAt(index);
        }
        public void ItemRemove(Item item)
        {
            Items.Remove(item);
        }
        public Item GetItem(int index)
        {
            return Items[index];
        }
    }
}
