using System.Collections.Generic;
using TextRPG.Entity;

namespace TextRPG.Data
{
    internal static class BattleContext
    {
        public static Character Player { get; set; }
        public static List<Monster> Monsters { get; set; } = new List<Monster>();
    }
}
