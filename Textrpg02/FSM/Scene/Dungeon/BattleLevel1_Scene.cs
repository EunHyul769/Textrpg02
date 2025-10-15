using System;
using System.Collections.Generic;
using TextRPG.Entity;
using TextRPG.Data;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class BattleLevel1_Scene : SceneBase
    {
        public BattleLevel1_Scene(SceneController controller) : base(controller) { }

        protected override void SetScene()
        {
            Console.Title = "Battle!!";

            var player = GameManager.Instance.Character;

            var monsters = new List<Monster>();
            int count = Random.Next(1, 5); 
            for (int i = 0; i < count; i++)
            {
                int randId = Random.Next(1, MonsterDB.Monsters.Count + 1);
                monsters.Add(MonsterDB.Monsters[randId].Clone());
            }

            BattleContext.Player = player;
            BattleContext.Monsters = monsters;

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
