using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Data;
using TextRPG.Data.DB;
using TextRPG.Entity;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class BossBattleScene : SceneBase
    {
        public BossBattleScene(SceneController controller) : base(controller) { }

        protected override void SetScene()
        {
            Console.Title = "Boss Battle!!";

            Character player = GameManager.Instance.Character;
            DungeonProgress progress = new DungeonProgress();

            // 층수 기반으로 보스 선택
            int bossKey = (progress.CurrentFloor / 5) % BossDB.Bosses.Count + 1;
            Monster boss = BossDB.Bosses[bossKey].Clone();

            List<Monster> bossList = new List<Monster> { boss };
            GameManager.Instance.MonsterList = bossList;

            Console.Clear();
            Console.WriteLine("=== Boss Battle ===\n");
            Console.WriteLine($"Lv.{boss.Id} {boss.Name}  HP {boss.Hp}");
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
