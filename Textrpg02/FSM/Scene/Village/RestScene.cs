using TextRPG.Entity;

namespace TextRPG.FSM.Scene.Village
{
    internal class RestScene : SceneBase
    {
        private Character character;

        public RestScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "휴식";
            character = GameManager.Instance.Character;
        }

        protected override void View()
        {
            Console.WriteLine("[휴식]\n");

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 휴식하기(500 G 소모)\n");
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
                    if (character.Gold >= 500)
                    {
                        int recoveredHp = 100;
                        int recoveredMp = 100;
                        int recoveredStamina = 50;

                        character.TakeGold(500);
                        character.AddHp(recoveredHp);
                        character.AddMp(recoveredMp);
                        character.AddStamina(recoveredStamina);
                        Console.WriteLine("휴식을 취했습니다.\n");
                        Console.WriteLine($"HP를 {recoveredHp} 회복하였습니다.");
                        Console.WriteLine($"MP를 {recoveredMp} 회복하였습니다.");
                        Console.WriteLine($"스테미나를 {recoveredStamina} 회복하였습니다.\n");
                        Console.WriteLine($"남은 골드: {character.Gold} G");
                        Console.WriteLine($"HP: {character.Hp}");
                        Console.WriteLine($"MP: {character.Mp}");
                        Console.WriteLine($"스테미나: {character.Stamina}");

                        ReturnToVillage();
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        Console.WriteLine($"보유 골드: {character.Gold} G");

                        ReturnToVillage();
                    }
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Sleep();
                    break;
            }
        }
    }
}
