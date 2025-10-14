using TextRPG.Entity;

namespace TextRPG.FSM.Scene
{
    internal class TrainingScene : SceneBase
    {
        private Character character;
        public TrainingScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "훈련하기";
            character = GameManager.Instance.Character;
        }

        protected override void View()
        {
            Console.WriteLine("[훈련하기]\n");

            if (character.TakeStamina(15))
            {
                int rand = Random.Next(1, 101);

                if (rand > 85)
                {
                    character.AddExp(60);
                    Console.WriteLine("훈련이 잘 되었습니다!");
                    Console.WriteLine($"Lv.: {character.Level}");
                    Console.WriteLine($"Exp: {character.Exp}");
                }
                else if (rand > 25)
                {
                    character.AddExp(40);
                    Console.WriteLine("오늘하루 열심히 훈련했습니다.");
                    Console.WriteLine($"Lv.: {character.Level}");
                    Console.WriteLine($"Exp: {character.Exp}");
                }
                else
                {
                    character.AddExp(30);
                    Console.WriteLine("하기 싫다... 훈련이...");
                    Console.WriteLine($"Lv.: {character.Level}");
                    Console.WriteLine($"Exp: {character.Exp}");
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
