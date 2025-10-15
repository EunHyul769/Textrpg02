namespace TextRPG.FSM.Scene.Dungeon
{
    internal class DungeonScene : SceneBase
    {
        public DungeonScene(SceneController controller) : base(controller)
        {
        }

        private int floorNum; //현재 층수
        private string dungeonName; //현재 던전 이름

        protected override void SetScene()
        {
            Console.Title = "던전 입장";

            switch (DungeonManager.Instance.dungeonStage)
            {
                case 0:
                    floorNum = 1;
                    dungeonName = "네메아 사자의 방";
                    break;
                case 1:
                    floorNum = 2;
                    dungeonName = "레나르의 호수";
                    break;
                case 2:
                    floorNum = 3;
                    dungeonName = "케리네이아의 야영지";
                    break;
                case 3:
                    floorNum = 4;
                    dungeonName = "에리만토스의 산림";
                    break;
                case 4:
                    floorNum = 5;
                    dungeonName = "하데스의 문";
                    break;
                default:
                    break;
            }
        }

        protected override void View()
        {
            
            Console.WriteLine("[던전 입장]\n");

            Console.WriteLine("0. 마을로 귀환");
            Console.WriteLine($"1. 던전으로 진입 ({floorNum}층: {dungeonName})\n");
        }

        protected override void Control()
        {
            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    DungeonManager.Instance.dungeonStage = 0; //층수 초기화
                    controller.ChangeSceneState(controller.VillageScene);
                    break;
                case "1":
                    Sleep();
                    Console.WriteLine("...터벅...");
                    Sleep();
                    Console.WriteLine("...터벅...");
                    Sleep();
                    Console.WriteLine("...터벅...");
                    Sleep();
                    controller.ChangeSceneState(controller.BattleScene); //전투 씬 진입
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Sleep();
                    break;
            }
        }
    }
}
