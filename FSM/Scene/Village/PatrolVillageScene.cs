using TextRPG.Entity;

namespace TextRPG.FSM.Scene.Village
{
    internal class PatrolVillageScene : SceneBase
    {
        private Character character;

        public PatrolVillageScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "마을 순찰";
            character = GameManager.Instance.Character;
        }

        protected override void View()
        {
            Console.WriteLine("[마을 순찰]\n");

            if (character.TakeStamina(5))
            {
                int rand = Random.Next(1, 101);

                if (rand > 90)
                {
                    character.TakeGold(500);
                    Console.WriteLine("마을 아이들이 모여있다. 간식을 사줘볼까?");
                    Console.WriteLine($"보유 골드: {character.Gold}");
                }
                else if (rand > 80)
                {
                    character.AddGold(2000);
                    Console.WriteLine("촌장님을 만나서 심부름을 했다.");
                    Console.WriteLine($"보유 골드: {character.Gold}");
                }
                else if (rand > 60)
                {
                    character.AddGold(1000);
                    Console.WriteLine("길 읽은 사람을 안내해주었다.");
                    Console.WriteLine($"보유 골드: {character.Gold}");
                }
                else if (rand > 30)
                {
                    character.AddGold(500);
                    Console.WriteLine("마을 주민과 인사를 나눴다. 선물을 받았다.");
                    Console.WriteLine($"보유 골드: {character.Gold}");
                }
                else
                {
                    Console.WriteLine("아무 일도 일어나지 않았다");
                }
            }
            else
            {
                Console.WriteLine("스태미나가 부족합니다.");
            }

            Console.WriteLine($"\n현재 스테미나: {character.Stamina}");

            ReturnToVillage();
        }

        protected override void Control()
        {
        }
    }
}
