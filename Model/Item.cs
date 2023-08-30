using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Define.GameEnum.eItemType Type { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
