using TextRPG.Entity;

namespace TextRPG
{
    internal class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        // 게임의 모든 데이터는 GameManager가 소유
        public Character Character { get; set; }
        public Shop Shop { get; set; }
        public bool IsGameOver { get; set; }

        public void InitializeCharacter(Character character)
        {
            Character = character;
        }
        
        public void InitializeShop(Shop shop)
        {
            Shop = shop;
        }
    }
}
