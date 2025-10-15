using System;
using System.Collections.Generic;
using System.Threading;
using TextRPG.Entity;
using TextRPG.Data;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class BattlePlayerTurn_Scene : SceneBase
    {
        private Character player;
        private List<Monster> monsters;

        public BattlePlayerTurn_Scene(SceneController controller) : base(controller) { }

        protected override void SetScene()
        {
            player = GameManager.Instance.Character;
            monsters = GameManager.Instance.MonsterList;
        }

        protected override void View()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            foreach (var m in monsters)
            {
                if (m.Hp <= 0)
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write($"Lv.{m.Id} {m.Name}  ");
                Console.WriteLine(m.Hp > 0 ? $"HP {m.Hp}" : "Dead");

                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 퇴각");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
        }

        protected override void Control()
        {
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowMonsterSelect();
                    break;
                case "2":
                    Console.WriteLine("\n스킬은 아직 구현되지 않았습니다.");
                    Thread.Sleep(700);
                    controller.ChangeSceneState(controller.BattleMonsterTurnScene);
                    break;
                case "3":
                    Retreat();
                    break;
                default:
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Thread.Sleep(700);
                    controller.ChangeSceneState(controller.BattlePlayerTurnScene);
                    break;
            }
        }

        private void ShowMonsterSelect()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                var m = monsters[i];
                if (m.Hp <= 0)
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write($"{i + 1} Lv.{m.Id} {m.Name}  ");
                Console.WriteLine(m.Hp > 0 ? $"HP {m.Hp}" : "Dead");

                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.Write("\n대상을 선택해주세요.\n>> ");

            string input = Console.ReadLine();
            if (input == "0")
            {
                controller.ChangeSceneState(controller.BattlePlayerTurnScene);
                return;
            }

            if (!int.TryParse(input, out int index) || index < 1 || index > monsters.Count)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Thread.Sleep(700);
                controller.ChangeSceneState(controller.BattlePlayerTurnScene);
                return;
            }

            var target = monsters[index - 1];
            if (target.Hp <= 0)
            {
                Console.WriteLine("\n이미 쓰러진 몬스터입니다.");
                Thread.Sleep(700);
                controller.ChangeSceneState(controller.BattlePlayerTurnScene);
                return;
            }

            double error = player.Attack * 0.1;
            int errorInt = (int)(error + 0.9999);
            int finalAttack = Random.Next(player.Attack - errorInt, player.Attack + errorInt + 1);

            int prevHp = target.Hp;
            target.Hp -= finalAttack;
            if (target.Hp < 0) target.Hp = 0;

            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine($"{player.Name} 의 공격!");
            Console.WriteLine($"Lv.{target.Id} {target.Name} 을(를) 맞췄습니다. [데미지 : {finalAttack}]");
            Console.WriteLine();
            if (target.Hp <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Lv.{target.Id} {target.Name}");
                Console.WriteLine($"HP {prevHp} -> Dead");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Lv.{target.Id} {target.Name}");
                Console.WriteLine($"HP {prevHp} -> {target.Hp}");
            }

            Thread.Sleep(1000);

            if (monsters.TrueForAll(m => m.Hp <= 0))
            {
                Console.WriteLine("\n전투에서 승리했습니다!");
                Thread.Sleep(1200);
                DungeonManager.Instance.dungeonResult = true; //승리 값 전달
                controller.ChangeSceneState(controller.DungeonResultScene); //결과 씬으로 전환
                return;
            }

            controller.ChangeSceneState(controller.BattleMonsterTurnScene);
        }

        private void Retreat()
        {
            DungeonManager.Instance.dungeonStage = 0; //던전 스테이지 초기화
            Console.WriteLine("\n전투에서 퇴각합니다...");
            Thread.Sleep(1000);
            controller.ChangeSceneState(controller.VillageScene);
        }
    }
}
