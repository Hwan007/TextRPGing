using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Model
{
    public class Equipment : Controller.EquipController
    {
        public int Id { get; set; }
        public List<Item> Items { get; private set; }
        public Equipment()
        {
            Items = new List<Item>();
        }
        public override void EquipItem(Item item)
        {
            int index = Items.FindIndex((x) => x == item);
            if (index != -1)
            {
                Items.RemoveAt(index);
                Character.Player.ReStat();
            }
            else
            {
                UnEquipType(item.Type);
                switch (item.Type)
                {
                    case Define.GameEnum.eItemType.Armor:
                        Items.Add(item);
                        break;
                    case Define.GameEnum.eItemType.Weapon:
                        Items.Add(item);
                        break;
                    case Define.GameEnum.eItemType.Potion:
                        //Items.Add(item);
                        break;
                    default:
                        throw new Exception($"{item.Type} 잘못된 정보입니다.");
                }
                Character.Player.ReStat();
            }
        }

        private void UnEquipType(Define.GameEnum.eItemType type)
        {
            foreach (var item in Items)
            {
                if (item.Type == type)
                {
                    Items.Remove(item);
                }
            }
        }
    }
}
