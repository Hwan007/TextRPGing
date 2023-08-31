using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;

namespace TextRPGing.Scene
{
    public class SInventory : IScene
    {
        public void MainLoop()
        {

        }

        public bool ActByInput(int input, ref Define.GameEnum.eSceneType scene)
        {
            return false;
        }
    }
}
