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
        public List<Item> Items { get; set; }

        public override void EquipItem(Item item)
        {
            base.EquipItem(item);
        }

        public override void UnEquipItem(int index)
        {
            base.UnEquipItem(index);
        }
    }
}
