using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class BattleLevel4_Scene : SceneBase
    {
        public BattleLevel4_Scene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "";
        }

        protected override void View()
        {
            Console.WriteLine("[던전 입장]\n");

            Console.WriteLine("1. 승리");
            Console.WriteLine("0. 패배\n");
        }

        protected override void Control()
        {
            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    DungeonLevel4_Scene.battleResult = false; //전투 결과 전달 (패배)
                    controller.ChangeSceneState(controller.DungeonLevel4_Scene);
                    break;
                case "1":
                    DungeonLevel4_Scene.battleResult = true; //전투 결과 전달 (승리)
                    controller.ChangeSceneState(controller.DungeonLevel4_Scene);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Sleep();
                    break;
            }
        }

    }
}
