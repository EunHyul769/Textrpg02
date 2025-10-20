
using TextRPG.Data.DB;

namespace TextRPG.Data
{
    internal class GameData
    {
        public static GameData Instance {  get; private set; }

        public ConsumeItemDB ConsumeItemDB { get; private set; }
        public EquipItemDB EquipItemDB { get; private set; }
        public MonsterDB MonsterDB { get; private set; }
        public SkillDB SkillDB { get; private set; }
        public JobSkillDB JobSkillDB { get; private set; }

        public GameData()
        {
            Instance = this;    //전역 공유
            ConsumeItemDB = new ConsumeItemDB();
            EquipItemDB = new EquipItemDB();
            MonsterDB = new MonsterDB();
            SkillDB = new SkillDB();
            JobSkillDB = new JobSkillDB();

        }


    }
}
