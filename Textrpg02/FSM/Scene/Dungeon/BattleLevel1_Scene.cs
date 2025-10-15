using System;
using System.Collections.Generic;
using TextRPG.Entity;
using TextRPG.Data;
using System.Threading;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class BattleLevel1_Scene : SceneBase
    {
        private Character player;
        private List<Monster> monsters;
        private bool isPlayerTurn = true;

        public BattleLevel1_Scene(SceneController controller) : base(controller) { }

        protected override void SetScene()
        {
            Console.Title = "Battle!!";

            player = GameManager.Instance.Character;
            monsters = new List<Monster>();

            int count = Random.Next(1, 5);
            for (int i = 0; i < count; i++)
            {
                int randId = Random.Next(1, MonsterDB.Monsters.Count + 1);
                monsters.Add(MonsterDB.Monsters[randId].Clone());
            }

            isPlayerTurn = true;
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

            if (isPlayerTurn)
            {
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. 퇴각");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            }
        }

        protected override void Control()
        {
            if (!isPlayerTurn)
                return;

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    PlayerAttack();
                    break;
                case "2":
                    UseSkill();
                    break;
                case "3":
                    Retreat();
                    return;
                default:
                    Console.WriteLine("\n잘못된 입력입니다.");
                    break;
            }
        }

        private void PlayerAttack()
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
            Console.WriteLine();
            Console.WriteLine("대상을 선택해주세요.");
            Console.Write(">> ");

            string input = Console.ReadLine();
            if (input == "0")
            {
                Console.WriteLine("\n공격을 취소했습니다.");
                return;
            }

            if (!int.TryParse(input, out int index) || index < 1 || index > monsters.Count)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                return;
            }

            Monster target = monsters[index - 1];
            if (target.Hp <= 0)
            {
                Console.WriteLine("\n잘못된 입력입니다. (이미 쓰러진 몬스터)");
                return;
            }

            double error = player.Attack * 0.1;
            int errorInt = (int)(error + 0.9999);
            int finalAttack = Random.Next(player.Attack - errorInt, player.Attack + errorInt + 1);

            int prevHp = target.Hp;
            target.Hp -= finalAttack;
            if (target.Hp < 0) target.Hp = 0;

            ShowAttackResult(target, prevHp, finalAttack);
        }

        private void ShowAttackResult(Monster target, int prevHp, int damage)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine($"{player.Name} 의 공격!");
            Console.WriteLine($"Lv.{target.Id} {target.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
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

            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.Write(">> ");

            while (true)
            {
                string next = Console.ReadLine();
                if (next == "0") break;
                Console.Write(">> ");
            }

            StartMonsterTurn();
        }

        private void StartMonsterTurn()
        {
            isPlayerTurn = false;
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("[몬스터의 턴]\n");

            bool anyAttack = false;

            foreach (var m in monsters)
            {
                if (m.Hp <= 0) continue;

                anyAttack = true;

                double error = m.Atk * 0.1;
                int errorInt = (int)(error + 0.9999);
                int finalDamage = Random.Next(m.Atk - errorInt, m.Atk + errorInt + 1);

                int prevHp = player.Hp;
                player.TakeHp(finalDamage);

                Console.WriteLine($"Lv.{m.Id} {m.Name} 의 공격!");
                Console.WriteLine($"{player.Name} 을(를) 맞았습니다. [데미지 : {finalDamage}]");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {prevHp} -> {player.Hp}");
                Console.WriteLine();

                if (player.Hp <= 0)
                {
                    Console.WriteLine("\n플레이어가 쓰러졌습니다...");
                    Console.WriteLine("전투 종료.");
                    Sleep();
                    controller.ChangeSceneState(controller.VillageScene);
                    return;
                }

                Thread.Sleep(500);
            }
            //if (!anyAttack) 여기서 승리신으로 넘어가면 되겠죠?

            while (true)
            {
                string next = Console.ReadLine();
                if (next == "0") break;
                Console.Write(">> ");
            }

            isPlayerTurn = true;
        }

        private void UseSkill()
        {
            Console.WriteLine("\n스킬을 선택했습니다!");
            Console.WriteLine("스킬시스템");
        }

        private void Retreat()
        {
            Console.WriteLine("\n전투에서 퇴각합니다...");
            Console.WriteLine("마을로 돌아갑니다.");
            Sleep();
            controller.ChangeSceneState(controller.VillageScene);
        }
    }
}
