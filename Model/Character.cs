using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Controller;
using TextRPGing.Define.Interface;

namespace TextRPGing.Model
{
    public class Character : CharacterController, IBattleStat
    {
        public static Character Player = null;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public float CRT { get; set; }
        public float AVD { get; set; }
        public Inventory Inven { get; set; }
        public Equipment Equip { get; set; }
        public Define.GameEnum.eCharacterClass Job { get; set; }
        private int xATK = 0;
        private int xDEF = 0;

        public Character(string name, Define.GameEnum.eCharacterClass job)
        {
            Name = name;
            Job = job;

            switch(Job) // 스탯 임의로 적어놨습니다. 랜덤으로 해도 재밌을거 같긴 하네요.
            {
                case Define.GameEnum.eCharacterClass.Warrior:
                    HP = 20;
                    MaxHP = 20;
                    ATK = 10;
                    DEF = 10;
                    CRT = 0.25f;
                    AVD = 0f;
                    break;
                case Define.GameEnum.eCharacterClass.Thief:
                    HP = 15;
                    MaxHP = 15;
                    ATK = 13;
                    DEF = 8;
                    CRT = 0.50f;
                    AVD = 0.25f;
                    break;
                case Define.GameEnum.eCharacterClass.Archer:
                    HP = 15;
                    MaxHP = 15;
                    ATK = 15;
                    DEF = 7;
                    CRT = 0.35f;
                    AVD = 0.15f;
                    break;
                case Define.GameEnum.eCharacterClass.Magician:
                    HP = 10;
                    MaxHP = 10;
                    ATK = 20;
                    DEF = 6;
                    CRT = 0.25f;
                    AVD = 0f;
                    break;
            }
            Inven = new Inventory();
            Equip = new Equipment(Player);

            Inven.Items.Add(new Potion(1, "포 션", Define.GameEnum.eItemType.Potion, "그저 포션일 뿐입니다.", 100));
            Inven.Items.Add(new Weapon("낡은 검", "초보자용 목검입니다.", 5, 0.10f, 100));
            Inven.Items.Add(new Armor("누더기 옷", "그냥 천 쪼가리 입니다.", 5, 0.10f, 100));
        }

        public override void ReStat()
        {
            ATK -= xATK;
            xATK = 0;
            DEF -= xDEF;
            xDEF = 0;
            foreach (Item item in Equip.Items)
            {
                if (item.Type == Define.GameEnum.eItemType.Weapon)
                {
                    xATK += ((Weapon)item).ATK;
                }
                else
                {
                    xDEF += ((Armor)item).DEF;
                }
                
            }

            ATK += xATK;
            DEF -= xDEF;
        }

        public override void TakeDamage(int  damage)
        {
            HP -= (int)((damage - DEF) * AVD); //임의로 데미지 계산 식 만들어놨습니다. 마음껏 변경하셔도 됩니다.
        }

        public override void TakeHeal(int heal)
        {
            HP += heal;
            if (HP > MaxHP)
                HP = MaxHP;
        }

    } 
}
