using TextRPG.Entity;
using TextRPG.Item;
using TextRPG.ShopSystem;

namespace TextRPG.FSM.Scene.Village
{
    internal class ShopScene : SceneBase
    {
        private Character character;
        private Shop shop = new Shop();

        public ShopScene(SceneController controller) : base(controller)
        {
            // 무기
            shop.AddItem(2000);
            shop.AddItem(2001);
            shop.AddItem(2002);

            // 방어구
            shop.AddItem(3000);
            shop.AddItem(3001);
            shop.AddItem(3002);
            shop.AddItem(3003);
            shop.AddItem(3004);
            shop.AddItem(3005);

            // 소비 아이템
            shop.AddItem(1000);
            shop.AddItem(1001);
            shop.AddItem(1002);
        }

        protected override void SetScene()
        {
            Console.Title = "상점";
            character = GameManager.Instance.Character;
            GameManager.Instance.InitializeShop(shop);
        }

        protected override void View()
        {
            Console.WriteLine("[상점]\n");

            Console.WriteLine($"보유 골드: {character.Gold}\n");

            Console.WriteLine("상점 - 장비 아이템\n");
            for (int i = 0; i < shop.EquipItemCount; i++)
            {
                if (shop.Items[i] is EquipItem item)
                {
                    string isBuy = item.IsBuy ? "[구매완료]" : $"{shop.Items[i].Price}G";
                    Console.WriteLine($"- {shop.Items[i].DisplayInfo()} | {isBuy}");
                }
                else
                {
                    Console.WriteLine($"- {shop.Items[i].DisplayInfo()} | {shop.Items[i].Price}G");
                }
            }
            Console.WriteLine();

            Console.WriteLine("상점 - 소비 아이템\n");
            for (int i = shop.EquipItemCount; i < shop.Items.Count; i++)
            {
                if (shop.Items[i] is ConsumeItem item)
                {
                    Console.WriteLine($"- {item.DisplayInfo()} | {shop.Items[i].Price}G");
                }
            }
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("3. 아이템 정렬\n");
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
                    controller.ChangeSceneState(controller.ShopBuyScene);
                    break;
                case "2":
                    controller.ChangeSceneState(controller.ShopSellScene);
                    break;
                case "3":
                    // 인벤토리 정렬 로직
                    // 타입 기반으로 아이템 이름이 긴 순으로 정렬
                    shop.Items.Sort((itemA, itemB) =>
                    {
                        // 장비 아이템 여부 확인 및 우선순위 설정
                        bool isAEquip = itemA is EquipItem;
                        bool isBEquip = itemB is EquipItem;

                        // 장비 아이템을 무조건 앞으로 배치합니다.
                        if (isAEquip != isBEquip)
                        {
                            // itemA가 장비 아이템이면 앞으로 (-1 반환)
                            if (isAEquip) return -1;

                            // itemB가 장비 아이템이면 뒤로 (1 반환, B를 앞으로 보내는 효과)
                            else return 1;
                        }

                        // 같은 타입(둘 다 장비이거나 둘 다 소비)일 경우 이름 길이로 정렬
                        // itemB의 길이와 itemA의 길이를 비교하여 내림차순 정렬 (긴 순서)
                        return itemB.Name.Length.CompareTo(itemA.Name.Length);
                    });
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Sleep();
                    break;
            }
        }
    }
}
