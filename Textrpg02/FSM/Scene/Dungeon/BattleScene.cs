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
            int count = Random.Next(1, 5);

            List<int> keys = new List<int>(MonsterDB.Monsters.Keys);

            for (int i = 0; i < count; i++)
            {
                int randKey = keys[Random.Next(keys.Count)];

                Monster newMonster = MonsterDB.Monsters[randKey].Clone();

                monsters.Add(newMonster);
            }

            GameManager.Instance.MonsterList = monsters;

            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                Monster monster = monsters[i];
                Console.WriteLine($"Lv.{monster.Id} {monster.Name}  HP {monster.Hp}");
            }

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
            Console.WriteLine();

            controller.ChangeSceneState(controller.BattlePlayerTurnScene);
        }

        protected override void View() 
        {
        
        }

        protected override void Control() 
        { 
        
        }
    }
}
