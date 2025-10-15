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
            for (int i = 0; i < count; i++)
            {
                int randId = Random.Next(1, MonsterDB.Monsters.Count + 1);
                monsters.Add(MonsterDB.Monsters[randId].Clone());
            }

            GameManager.Instance.MonsterList = monsters;

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
