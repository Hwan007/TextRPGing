namespace TextRPGing.Define
{
    public class GameEnum
    {
        public enum eSceneType
        {
            Town,
            Status,
            Battle,
            Recovery,
            SaveLoad,
            End
        }


        public enum eItemType
        {
            Potion,
            Armor,
            Weapon
        }

        public enum eSkillType
        {
            warrior_skill,
            thief_skill,
            archor_skill,
            magician_skill
        }

        public enum eCharacterClass
        {
            Warrior,
            Thief,
            Archer,
            Magician
        }
    }
}
