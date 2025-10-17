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
            List<int> keys = new List<int>(MonsterDB.Monsters.Keys);
            int floor = GameManager.Instance.CurrentFloors;

            // 1. 일반 몬스터 구성
            int count = Random.Next(1, 5);
            for (int i = 0; i < count; i++)
            {
                int randKey = keys[Random.Next(keys.Count)];
                Monster newMonster = MonsterDB.Monsters[randKey].Clone();
                monsters.Add(newMonster);
            }

            // 2. 현재 층이 10의 배수라면 보스 몬스터 추가
            if (floor % 10 == 0)
            {
                int bossKey = floor; // 예: 10층 -> ID 10, 20층 -> ID 20
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
                    Console.WriteLine($"\n⚠ MonsterDB에 보스 ID {bossKey}가 없습니다. 기본 몬스터로 대체합니다.");
                    Console.ResetColor();
                }
            }

            // 3. 전투 세팅
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
