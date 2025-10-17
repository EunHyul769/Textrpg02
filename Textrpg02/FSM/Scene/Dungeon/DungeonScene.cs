namespace TextRPG.FSM.Scene.Dungeon
{
    internal class DungeonScene : SceneBase
    {
        public DungeonScene(SceneController controller) : base(controller) { }

        private int floorNum; //현재 층수

        protected override void SetScene()
        {
            Console.Title = "던전 입장";
        }

        protected override void View()
        {
            floorNum = GameManager.Instance.CurrentFloors; //현재 층수
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

                    // 🔹 보스 씬 대신 일반 전투 씬으로만 이동
                    controller.ChangeSceneState(controller.BattleScene);
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Sleep();
                    break;
            }
        }
    }
}
