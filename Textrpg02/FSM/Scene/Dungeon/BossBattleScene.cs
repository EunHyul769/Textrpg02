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

            // 🧠 층수에 따라 보스 ID 선택 (MonsterDB 안의 ID 사용)
            // 예: 5층 → 100, 10층 → 200, 15층 → 300 ...
            int bossId = progress.CurrentFloor switch
            {
                5 => 100,
                10 => 200,
                15 => 300,
                _ => 100 // 기본 보스 ID
            };

            if (!MonsterDB.Monsters.ContainsKey(bossId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[오류] MonsterDB에 보스 ID {bossId}가 존재하지 않습니다!");
                Console.ResetColor();
                Console.WriteLine("기본 보스로 대체합니다.");
                bossId = 1; // 예비용 ID
            }

            Monster boss = MonsterDB.Monsters[bossId].Clone();

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
// 이젬 몬스터 DB에 100, 200, 300 ID를 보스 몬스터로 인지합니다