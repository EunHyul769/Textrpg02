
using TextRPG.Entity;
using TextRPG.Manager;

namespace TextRPG.FSM.Scene.Village
{
    internal class VillageScene : SceneBase
    {
        private Character character;
        private Inventory inventory;

        public VillageScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "영웅의 시련";
            Console.ReadLine();
            character = GameManager.Instance.Character;
            inventory = GameManager.Instance.Character.Inventory;
        }

        protected override void View()
        {
            Console.WriteLine("[영웅의 마을, 아테네]");
            Console.WriteLine("오늘도 영웅을 꿈꾸는 젊은이가 왔군.");
            Console.WriteLine("준비를 게을리하면 목숨을 잃기 쉽상이네. 귀한 목숨을 허투루 하지 말게나.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 랜덤 모험(-스테미나 10)");
            Console.WriteLine("4. 마을 순찰(-스테미나 5)");
            Console.WriteLine("5. 훈련하기(-스테미나 15)");
            Console.WriteLine("6. 상점");
            Console.WriteLine("7. 시련의 탑 입장");
            Console.WriteLine("8. 휴식하기(-500 G)");
            Console.WriteLine("9. 저장 및 게임 종료");
            Console.WriteLine("10. 데이터 불러오기\n");
        }

        protected override void Control()
        {
            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    controller.ChangeSceneState(controller.StatusScene);
                    break;
                case "2":
                    controller.ChangeSceneState(controller.InventoryScene);
                    break;
                case "3":
                    controller.ChangeSceneState(controller.RandomAdventureScene);
                    break;
                case "4":
                    controller.ChangeSceneState(controller.PatrolVillageScene);
                    break;
                case "5":
                    controller.ChangeSceneState(controller.TrainingScene);
                    break;
                case "6":
                    controller.ChangeSceneState(controller.ShopScene);
                    break;
                case "7":
                    controller.ChangeSceneState(controller.DungeonScene);
                    break;
                case "8":
                    controller.ChangeSceneState(controller.RestScene);
                    break;
                case "9":
                    DataManager.Instance.SaveGame(character, inventory);
                    GameManager.Instance.IsGameOver = true;
                    break;
                case "10":
                    if (DataManager.Instance.LoadGame(out character, out inventory))
                    {
                        GameManager.Instance.InitializeCharacter(character);
                        Console.WriteLine("데이터 로딩에 성공하였습니다.");
                    }
                    else
                        Console.WriteLine("데이터 로딩에 실패하였습니다.");
                    Sleep();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Sleep();
                    break;
            }
        }
    }
}
