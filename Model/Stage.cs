using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Model
{
    public class Stage
    {
        public Monster[] Monsters { get; set; }
        public string Name { get; set; }
        public int Difficulty { get; set; }

    }
}
