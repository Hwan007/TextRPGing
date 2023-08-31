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
            Inventory,
            SaveLoad,
            Store,
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
            Potion,
            Armor,
            Weapon
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
