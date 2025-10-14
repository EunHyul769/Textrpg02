using TextRPG.Entity;

namespace TextRPG.FSM.Scene.PlayerScene
{
    internal class StatusScene : SceneBase
    {
        private Character character;

        public StatusScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "상태창";
            character = GameManager.Instance.Character;
        }

        protected override void View()
        {
            Console.WriteLine("[상태창]");
            Console.WriteLine("캐릭터의 능력치를 확인할 수 있습니다.\n");

            Console.WriteLine(character.DisplayInfo());

            ReturnToVillage();
        }

        protected override void Control()
        {
        }
    }
}
