using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class DungeonProgress
    {
        public int CurrentFloor => GameManager.Instance.CurrentFloors;

        // 예: 5층마다 보스전
        public bool IsBossFloor() => CurrentFloor % 5 == 0;

        public void ShowProgress()
        {
            Console.WriteLine($"현재 층수: {CurrentFloor}");
            Console.WriteLine(IsBossFloor() ? "이번 층은 보스전입니다!" : "일반 전투 층입니다.");
        }

        public void NextFloor()
        {
            GameManager.Instance.CurrentFloors++;
        }
    }
}
