using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Entity;

namespace TextRPG.Data.DB
{
    internal class BossDB
    {
        public static Dictionary<int, Monster> Bosses = new Dictionary<int, Monster>()
        {
            // id, name, hp, atk, def, spd, exp, gold
            { 1, new Monster(1, "보스 A", 120, 15, 5, 8, 50, 100) },
            { 2, new Monster(2, "보스 B", 220, 20, 8, 6, 100, 200) },
            { 3, new Monster(3, "보스 C", 350, 25, 10, 5, 150, 300) },
            { 4, new Monster(4, "보스 D", 280, 30, 7, 10, 200, 400) },
            { 5, new Monster(5, "보스 E", 500, 40, 15, 12, 500, 1000) }
        };
    }
}
