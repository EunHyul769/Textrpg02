using TextRPG.Entity;
using TextRPG.Item;

namespace TextRPG.FSM.Scene
{
    internal class ShopSellScene : SceneBase
    {
        private Character character;
        private Inventory inventory;

        public ShopSellScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "상점 - 아이템 판매";
            character = GameManager.Instance.Character;
            inventory = GameManager.Instance.Character.Inventory;
        }

        protected override void View()
        {
            Console.WriteLine("[상점 - 아이템 판매]\n");

            Console.WriteLine("구매 가격의 85% 가격에 판매할 수 있습니다.\n");

            Console.WriteLine($"보유 골드: {character.Gold}\n");

            Console.WriteLine("상점 - 보유 장비 아이템\n");
            for (int i = 0; i < inventory.EquipItemCount; i++)
            {
                Console.WriteLine($"- {i + 1} {inventory.Items[i].DisplayInfo()} | {inventory.Items[i].Price}G");
            }
            Console.WriteLine();

            Console.WriteLine("상점 - 보유 소비 아이템\n");
            for (int i = inventory.EquipItemCount; i < inventory.Items.Count; i++)
            {
                Console.WriteLine($"- {i + 1} {inventory.Items[i].DisplayInfo()} | {inventory.Items[i].Price}G");
            }
            Console.WriteLine();

            Console.WriteLine("0. 상점으로 돌아가기\n");
        }

        protected override void Control()
        {
            Console.Write("판매할 아이템 번호를 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                controller.ChangeSceneState(controller.ShopScene);
                return;
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= inventory.Items.Count)
            {
                // 유효한 인덱스 선택 (배열 인덱스는 0부터 시작하므로 -1)
                ItemBase selectedItem = inventory.Items[choice - 1];

                inventory.SellItem(selectedItem);
                character.AddGold((int)(selectedItem.Price * 0.85f));
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. ");
            }

            Sleep();
        }
    }
}
