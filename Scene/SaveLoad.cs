using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define;
using TextRPGing.Define.Interface;

namespace TextRPGing.Scene
{
    public class SaveLoad : IScene
    {
        public bool ActByInput(int input)
        {
            // TODO
            return false;
        }

        public bool ActByInput(int input, ref GameEnum.eSceneType scene)
        {
            throw new NotImplementedException();
        }

        public void MainLoop()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
