using TextRPG.Enum;

namespace TextRPG.Data
{
    internal class CharacterData
    {
        // 생성자 주입
        public string Name { get; set; }
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public int MaxMp { get; set; }
        public int Mp { get; set; }
        public int Attack { get; set; }
        public int SkillAttack { get; set; }
        public int Armor { get; set; }
        public int MagicResistance { get; set; }
        public JobType Job { get; set; }

        // 초기 세팅값
        public int Level { get; set; }
        public int Gold { get; set; }
        public int MaxExp { get; set; }
        public int Exp { get; set; }
        public int Stamina { get; set; }

        // 아이템으로 얻은 추가 능력치
        public int BonusMaxHp { get; set; }
        public int BonusMaxMp { get; set; }
        public int BonusAttack { get; set; }
        public int BonusSkillAttack { get; set; }
        public int BonusArmor { get; set; }
        public int BonusMagicResistance { get; set; }
    }
}
