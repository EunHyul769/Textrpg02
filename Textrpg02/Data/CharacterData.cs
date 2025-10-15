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

        // 아이템으로 얻은 추가 능력치
        public int bonusMaxHp;
        public int bonusMaxMp;
        public int bonusAttack;
        public int bonusSkillAttack;
        public int bonusArmor;
        public int bonusMagicResistance;

        public List<int> learnedSkills = new List<int>();
    }
}
