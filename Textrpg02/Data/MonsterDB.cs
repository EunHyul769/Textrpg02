using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Entity;

namespace TextRPG.Data
{
    internal class MonsterDB
    {
        //몬스터 딕셔너리
        public static Dictionary<int, Monster> Monsters = new Dictionary<int, Monster>()
        {   //            id  이름            체 공 방 속도 exp gold
            {1, new Monster(1, "더미A",       30, 30, 1, 1, 10, 10)},
            {2, new Monster(1, "더미B",       40, 30, 1, 1, 10, 10)},
        };
    }
}
