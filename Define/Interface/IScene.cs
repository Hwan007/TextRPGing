using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Define.Interface
{
    public interface IScene
    {
        public void MainLoop();
        public void ActByInput(int input);
    }
}
