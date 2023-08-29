using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Define.Interface
{
    internal class IAction
    {
        public string Name { get; set; }
        public ActionResult? result { get; set; }

    }

    public class ActionResult : IAction
    {

    }
}
