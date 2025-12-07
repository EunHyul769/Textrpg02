namespace TextRPG.FSM.Scene.Dungeon
{
    internal class DungeonScene : SceneBase
    {
        public DungeonScene(SceneController controller) : base(controller) { }

        private int floorNum; //현재 층수

        protected override void SetScene()
        {
            Console.Title = "시련의 탑 입장";
        }

        protected override void View()
        {
<<<<<<< Updated upstream
            floorNum = GameManager.Instance.CurrentFloors; //현재 층수
            Console.WriteLine("[시련의 탑 입장]\n");
            Console.WriteLine("0. 마을로 귀환");
            Console.WriteLine($"1. 시련의 탑으로 진입 ({floorNum}층)\n");
=======
            Console.WriteLine("[던전 입장]\n");

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 던전에 진입하기\n");
            //Console.WriteLine("1. 쉬운 던전 | 방어력 5 이상 권장");
            //Console.WriteLine("2. 일반 던전 | 방어력 11 이상 권장");
            //Console.WriteLine("3. 어려운 던전 | 방어력 17 이상 권장\n");
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                    Sleep();
                    Console.WriteLine("...터벅...");
                    Sleep();
                    Console.WriteLine("...터벅...");
                    Sleep();
                    Console.WriteLine("...터벅...");
                    Sleep();

                    // 보스 씬 대신 일반 전투 씬으로만 이동
                    controller.ChangeSceneState(controller.BattleScene);
                    break;

=======
                    DungeonRewardScene.LevelSetting(1);
                    controller.ChangeSceneState(controller.DungeonLevel1_Scene);
                    break;
                //case "2":
                //    DungeonRewardScene.LevelSetting(2);
                //    controller.ChangeSceneState(controller.DungeonRewardScene);
                //    break;
                //case "3":
                //    DungeonRewardScene.LevelSetting(3);
                //    controller.ChangeSceneState(controller.DungeonRewardScene);
                //    break;
>>>>>>> Stashed changes
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Sleep();
                    break;
            }
        }
    }
}
