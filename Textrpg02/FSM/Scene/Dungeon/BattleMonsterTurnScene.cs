using System;
using System.Collections.Generic;
using System.Threading;
using TextRPG.Entity;
using TextRPG.Data;
using TextRPG.Calculator;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class BattleMonsterTurn_Scene : SceneBase
    {
        private Character player;
        private List<Monster> monsters;

        public BattleMonsterTurn_Scene(SceneController controller) : base(controller) { }

        protected override void SetScene()
        {
            player = GameManager.Instance.Character;
            monsters = GameManager.Instance.MonsterList;
        }

        protected override void View()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("[몬스터의 턴]\n");

            foreach (Monster m in monsters)
            {
                if (m.Hp <= 0) continue;

                int finalDamage = DamageCalculator.CalculateAttack(m, player);

                int prevHp = player.Hp;
                player.TakeHp(finalDamage);

                Console.WriteLine($"{m.Name}의 공격! → {player.Name}에게 {finalDamage} 피해!");
                Console.WriteLine($"플레이어 HP: {prevHp} → {player.Hp}\n");

                Thread.Sleep(700);

                if (player.Hp <= 0)
                {
                    Console.WriteLine("\n플레이어가 쓰러졌습니다...");
                    Thread.Sleep(1000);
                    controller.ChangeSceneState(controller.DungeonResultScene);
                    return;
                }
            }

            Thread.Sleep(500);
            controller.ChangeSceneState(controller.BattlePlayerTurnScene);
        }

        protected override void Control() { }
    }
}
