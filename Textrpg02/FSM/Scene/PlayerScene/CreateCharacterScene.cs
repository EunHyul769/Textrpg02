using TextRPG.Entity;
using TextRPG.Enum;

namespace TextRPG.FSM.Scene.PlayerScene
{
    internal class CreateCharacterScene : SceneBase
    {
        private Character Character;
        private Shop shop;

        public CreateCharacterScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "캐릭터 생성";
        }

        protected override void View()
        {
            Console.WriteLine("캐릭터의 이름을 입력해주세요.");
            string? name = Console.ReadLine();
            Console.Clear();

            if (!string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine($"반갑습니다 {name}님, TextRPG에 오신 것을 환영합니다!\n");

                Console.WriteLine("[직업 선택]\n");

                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 궁수");
                Console.WriteLine("3. 마법사\n");

                Console.WriteLine("원하시는 직업을 선택해주세요.");
                string? job = Console.ReadLine();
                Console.Clear();

                switch (job)
                {
                    // 전사
                    case "1":
                        Character = new Character(name, 100, 20, 10, 3, 10, 10, JobType.Warrior);
                        GameManager.Instance.InitializeCharacter(Character);
                        controller.ChangeSceneState(controller.VillageScene);
                        break;

                    // 궁수
                    case "2":
                        Character = new Character(name, 80, 40, 8, 5, 8, 8, JobType.Archer);
                        GameManager.Instance.InitializeCharacter(Character);
                        controller.ChangeSceneState(controller.VillageScene);
                        break;

                    // 마법사
                    case "3":
                        Character = new Character(name, 60, 60, 6, 10, 6, 6, JobType.Mage);
                        GameManager.Instance.InitializeCharacter(Character);
                        controller.ChangeSceneState(controller.VillageScene);
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Sleep();
                        break;
                }
            }
            else
            {
                Console.WriteLine("불가능한 이름입니다.");
                Sleep();
            }
        }

        protected override void Control()
        {
        }
    }
}
