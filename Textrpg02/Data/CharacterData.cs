using TextRPG.Enum;

namespace TextRPG.Data
{
    internal class CharacterData
    {
        // 생성자 주입
        public string name;
        public int maxHp;
        public int hp;
        public int maxMp;
        public int mp;
        public int attack;
        public int skillAttack;
        public int armor;
        public int magicResistance;
        public JobType job;

        // 초기 세팅값
        public int level;
        public int gold;
        public int maxExp;
        public int exp;
        public int stamina;

        public List<int> learnedSkills = new List<int>();
    }
}
