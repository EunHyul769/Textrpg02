using TextRPG.Entity;
using TextRPG.Item;

namespace TextRPG.FSM.Scene
{
    internal class ShopBuyScene : SceneBase
    {
        private Character character;
        private Inventory inventory;
        private Shop shop;

        public ShopBuyScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "상점 - 아이템 구매";
            character = GameManager.Instance.Character;
            inventory = GameManager.Instance.Character.Inventory;
            shop = GameManager.Instance.Shop;
        }

        protected override void View()
        {
            Console.WriteLine("[상점 - 아이템 구매]\n");

            Console.WriteLine($"보유 골드: {character.Gold}\n");

            Console.WriteLine("상점 - 장비 아이템\n");
            for (int i = 0; i < shop.EquipItemCount; i++)
            {
                if (shop.Items[i] is EquipItem item)
                {
                    string isBuy = item.IsBuy ? "[구매완료]" : $"{shop.Items[i].Price}G";
                    Console.WriteLine($"- {i + 1} {shop.Items[i].DisplayInfo()} | {isBuy}");
                }
                else
                {
                    Console.WriteLine($"- {i + 1} {shop.Items[i].DisplayInfo()} | {shop.Items[i].Price}G");
                }
            }
            Console.WriteLine();

            Console.WriteLine("상점 - 소비 아이템\n");
            for (int i = shop.EquipItemCount; i < shop.Items.Count; i++)
            {
                if (shop.Items[i] is ConsumeItem item)
                {
                    Console.WriteLine($"- {i + 1} {item.DisplayInfo()} | {shop.Items[i].Price}G");
                }
            }
            Console.WriteLine();

            Console.WriteLine("0. 상점으로 돌아가기\n");
        }

        protected override void Control()
        {
            Console.Write("구매할 아이템 번호를 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                controller.ChangeSceneState(controller.ShopScene);
                return;
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= shop.Items.Count)
            {
                // 유효한 인덱스 선택 (배열 인덱스는 0부터 시작하므로 -1)
                ItemBase selectedItem = shop.Items[choice - 1];

                if (character.Gold >= selectedItem.Price)
                {
                    if (selectedItem is EquipItem item)
                    {
                        if (!item.IsBuy)
                        {
                            character.TakeGold(selectedItem.Price);
                            inventory.AddItem(selectedItem);
                            Console.WriteLine("아이템을 구매하였습니다.");
                        }
                        else
                        {
                            Console.WriteLine("이미 구매한 아이템입니다.");
                        }
                    }
                    else
                    {
                        character.TakeGold(selectedItem.Price);
                        inventory.AddItem(selectedItem);
                        Console.WriteLine("아이템을 구매하였습니다.");
                    }
                }
                else
                {
                    Console.WriteLine("골드가 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. ");
            }

            Sleep();
        }
    }
}
