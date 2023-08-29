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
        private Character mCharacter;
        public Equipment(Character character)
        {
            mCharacter = character;
        }
        public override void EquipItem(Item item)
        {
            int index = Items.FindIndex((x) => x == item);
            if (index != -1)
            {
                Items.RemoveAt(index);
                mCharacter.ReStat();
            }
            else
            {
                UnEquipType(item.type);
                switch (item.type)
                {
                    case Define.Enum.eItemType.Armor:
                        Items.Add(item);
                        break;
                    case Define.Enum.eItemType.Weapon:
                        Items.Add(item);
                        break;
                    default:
                        throw new Exception($"{item.type} 잘못된 정보입니다.");
                }
                mCharacter.ReStat();
            }
        }

        private void UnEquipType(Define.Enum.eItemType type)
        {
            foreach (var item in Items)
            {
                if (item.type == type)
                {
                    Items.Remove(item);
                }
            }
        }
    }
}
