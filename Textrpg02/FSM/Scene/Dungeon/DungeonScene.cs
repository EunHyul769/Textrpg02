namespace TextRPG.FSM.Scene.Dungeon
{
    internal class DungeonScene : SceneBase
    {
        public DungeonScene(SceneController controller) : base(controller)
        {
        }

        private int floorNum = GameManager.Instance.CurrentFloors; //현재 층수

        protected override void SetScene()
        {
            Console.Title = "던전 입장";
        }

        protected override void View()
        {
            
            Console.WriteLine("[던전 입장]\n");

            Console.WriteLine("0. 마을로 귀환");
            Console.WriteLine($"1. 던전으로 진입 ({floorNum}층)\n");
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
