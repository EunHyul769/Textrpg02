namespace TextRPG.FSM.Scene.Dungeon
{
    internal class DungeonScene : SceneBase
    {
        public DungeonScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "던전 입장";
        }

        protected override void View()
        {
            Console.WriteLine("[던전 입장]\n");

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 쉬운 던전 | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전 | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전 | 방어력 17 이상 권장\n");
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
                    DungeonRewardScene.LevelSetting(1);
                    controller.ChangeSceneState(controller.DungeonRewardScene);
                    break;
                case "2":
                    DungeonRewardScene.LevelSetting(2);
                    controller.ChangeSceneState(controller.DungeonRewardScene);
                    break;
                case "3":
                    DungeonRewardScene.LevelSetting(3);
                    controller.ChangeSceneState(controller.DungeonRewardScene);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Sleep();
                    break;
            }
        }
    }
}
