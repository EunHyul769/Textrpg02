using System;
using System.Collections.Generic;
using TextRPG.Entity;
using TextRPG.Data;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class BattleScene : SceneBase
    {
        public BattleScene(SceneController controller) : base(controller) { }

        protected override void SetScene()
        {
            Console.Title = "Battle!!";

            Character player = GameManager.Instance.Character;
            List<Monster> monsters = new List<Monster>();

            int floor = GameManager.Instance.CurrentFloors;

            // 🧩 층수 기반 몬스터 그룹 계산 (1~3, 4~6, 7~9 ...)
            int groupStart = ((floor - 1) / 3) * 3 + 1;
            int groupEnd = groupStart + 2;

            List<int> availableKeys = new List<int>();
            for (int i = groupStart; i <= groupEnd; i++)
            {
                if (MonsterDB.Monsters.ContainsKey(i))
                    availableKeys.Add(i);
            }

            // 예비용: 구간 몬스터가 없으면 전체 허용
            if (availableKeys.Count == 0)
                availableKeys.AddRange(MonsterDB.Monsters.Keys);

            // 🧩 보스 여부 체크
            bool isBossFloor = (floor % 3 == 0);

            // 🧩 보스층이면 일반 몬스터 생략
            if (!isBossFloor)
            {
                int count = Random.Next(1, 4); // 일반 몬스터 개수 (1~3마리)
                for (int i = 0; i < count; i++)
                {
                    int randKey = availableKeys[Random.Next(availableKeys.Count)];
                    Monster newMonster = MonsterDB.Monsters[randKey].Clone();
                    monsters.Add(newMonster);
                }
            }

            // 🧩 3층마다 100단위 보스 생성
            if (isBossFloor)
            {
                int bossKey = (floor / 3) * 100; // 3층→100, 6층→200, 9층→300 ...

                if (MonsterDB.Monsters.ContainsKey(bossKey))
                {
                    Monster boss = MonsterDB.Monsters[bossKey].Clone();
                    monsters.Add(boss);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n[보스 등장!] {boss.Name} 이(가) 나타났다!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n⚠ MonsterDB에 보스 ID {bossKey}가 없습니다.");
                    Console.ResetColor();
                }
            }

            // 🧩 전투 세팅
            GameManager.Instance.MonsterList = monsters;

            Console.Clear();
            Console.WriteLine(isBossFloor ? "=== Boss Battle ===\n" : "Battle!!\n");

            foreach (var monster in monsters)
                Console.WriteLine($"Lv.{monster.Id} {monster.Name}  HP {monster.Hp}");

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
            Console.WriteLine();

            controller.ChangeSceneState(controller.BattlePlayerTurnScene);
        }

        protected override void View() { }
        protected override void Control() { }
    }  
}
