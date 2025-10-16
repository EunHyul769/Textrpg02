using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class DungeonManager
    {
        private static DungeonManager instance;
        public static DungeonManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DungeonManager();
                }
                return instance;
            }
        }

        public bool dungeonResult = false;

    }
}
