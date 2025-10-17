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
            List<int> availableKeys = new List<int>();

            // 🧩 층수 구간별 일반 몬스터 제한
            if (floor >= 1 && floor <= 3)
            {
                availableKeys.AddRange(new[] { 1, 2, 3 });
            }
            else if (floor >= 4 && floor <= 6)
            {
                availableKeys.AddRange(new[] { 4, 5, 6 });
            }
            else
            {
                // 그 이후층도 대비해서 기본값으로 전체 키를 허용
                availableKeys.AddRange(MonsterDB.Monsters.Keys);
            }

            int count = Random.Next(1, 4); // 일반 몬스터 개수 (1~3마리)

            // 🧩 일반 몬스터 생성
            for (int i = 0; i < count; i++)
            {
                int randKey = availableKeys[Random.Next(availableKeys.Count)];
                Monster newMonster = MonsterDB.Monsters[randKey].Clone();
                monsters.Add(newMonster);
            }

            // 🧩 3층마다 100단위 보스 추가
            if (floor % 3 == 0)
            {
                int bossKey = (floor / 3) * 100; // 3층→100, 6층→200, 9층→300 ...

                if (MonsterDB.Monsters.ContainsKey(bossKey))
                {
                    Monster boss = MonsterDB.Monsters[bossKey].Clone();
                    monsters.Add(boss);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n보스 '{boss.Name}' 이(가) 전투에 등장했습니다!");
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
            Console.WriteLine("Battle!!\n");

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
