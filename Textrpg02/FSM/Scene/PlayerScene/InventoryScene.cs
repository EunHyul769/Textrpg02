using TextRPG.Entity;
using TextRPG.Item;

namespace TextRPG.FSM.Scene
{
    internal class InventoryScene : SceneBase
    {
        private Inventory inventory;

        public InventoryScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "인벤토리";
            inventory = GameManager.Instance.Character.Inventory;
        }

        protected override void View()
        {
            Console.WriteLine("[인벤토리]\n");

            Console.WriteLine("[인벤 - 장비 아이템]\n");
            for (int i = 0; i < inventory.Items.Count; i++)
            {
                if (inventory.Items[i] is EquipItem item)
                {
                    string isEquipped = item.IsEquipped ? " [E]" : "";
                    Console.WriteLine($"- {isEquipped} {item.DisplayInfo()}");
                }
            }
            Console.WriteLine();

            Console.WriteLine("[인벤 - 소비 아이템]\n");
            for (int i = inventory.EquipItemCount; i < inventory.Items.Count; i++)
            {
                if (inventory.Items[i] is ConsumeItem item)
                {
                    Console.WriteLine($"- {item.DisplayInfo()}");
                }
            }
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장비 아이템 관리");
            Console.WriteLine("2. 소비 아이템 관리");
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
                    controller.ChangeSceneState(controller.EquipmentScene);
                    break;
                case "2":
                    controller.ChangeSceneState(controller.ConsumptionScene);
                    break;
                case "3":
                    // 인벤토리 정렬 로직
                    // 타입 기반으로 아이템 이름이 긴 순으로 정렬
                    inventory.Items.Sort((itemA, itemB) =>
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
