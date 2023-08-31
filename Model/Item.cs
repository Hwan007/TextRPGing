﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define;

namespace TextRPGing.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Define.GameEnum.eItemType Type { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public Item(int id, string name, GameEnum.eItemType type, string description, int price)
        {
            Id = id;
            Name = name;
            Type = type;
            Description = description;
            Price = price;
        }
    }
}
