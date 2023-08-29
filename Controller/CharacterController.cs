using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;

namespace TextRPGing.Controller
{
    public abstract class CharacterController
    {
        public abstract void ReStat();
        //캐릭터 스탯 재계산

        public abstract void TakeDamage(int damage);
        //받은 데미지 계산           

        public abstract void TakeHeal(int heal);
            //체력 회복

        public IAction[] GetEnableAction()
        {
            return new IAction[] { };
        }
    }
}
