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
            {1, new Monster(1, "더미A",       20, 10, 1, 1, 20, 50)},
            {2, new Monster(2, "더미B",       25, 12, 1, 1, 30, 80)},
            {3, new Monster(3, "더미C",       30, 15, 1, 1, 40, 100)},




            //========보스몹===============

            //{101, new Monster(101, "보스더미A", 150, 35, 10, 10, 100, 350)},

        };
    }
}
