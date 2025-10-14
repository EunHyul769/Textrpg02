using TextRPG.FSM;

namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SceneController sceneController = new SceneController();
            sceneController.Start();

            // 메인 게임 루프
            while (!GameManager.Instance.IsGameOver)
            {
                sceneController.Update();
            }

            Console.WriteLine("\n게임이 종료되었습니다.");
            Console.WriteLine("플레이해주셔서 감사합니다!");
        }
    }
}
