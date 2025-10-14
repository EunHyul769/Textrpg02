using TextRPG.Entity;

namespace TextRPG.Data
{
    internal class GameData
    {
        public Character Character { get; set; }



        //몬스터 딕셔너리
        public static Dictionary<int, Monster> Monsters = new Dictionary<int, Monster>()
        {
            {1, new Monster(1, "더미A",       30, 30, 1, 1, 10, 10)},
            {2, new Monster(1, "더미B",       40, 30, 1, 1, 10, 10)},
        };
    }
}
