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

        public Stage()
        {
            Monsters = new Monster[3];

            Monsters[0] = new Monster("허수아비0",1,1,1,1,1);
            Monsters[1] = new Monster("허수아비1",2,2,2,2,2);
            Monsters[2] = new Monster("허수아비2",3,3,3,3,3);

        }

    }
}
