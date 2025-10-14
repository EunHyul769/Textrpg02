using TextRPG.Entity;
using TextRPG.Item;

namespace TextRPG.FSM.Scene.PlayerScene
{
    internal class ConsumptionScene : SceneBase
    {
        private Inventory inventory;

        public ConsumptionScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "인벤토리 - 소비 아이템 관리";
            inventory = GameManager.Instance.Character.Inventory;
        }

        protected override void View()
        {
            Console.WriteLine("[인벤 - 소비 아이템]\n");
            for (int i = inventory.EquipItemCount; i < inventory.Items.Count; i++)
            {
                if (inventory.Items[i] is ConsumeItem item)
                {
                    Console.WriteLine($"- {i + 1 - inventory.EquipItemCount} {item.DisplayInfo()}");
                }
            }
            Console.WriteLine();

            Console.WriteLine("0. 인벤토리로 돌아가기\n");
        }

        protected override void Control()
        {
            Console.Write("사용할 아이템 번호를 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                controller.ChangeSceneState(controller.InventoryScene);
                return;
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= inventory.Items.Count - inventory.EquipItemCount)
            {
                // 유효한 인덱스 선택
                ConsumeItem selectedItem = (ConsumeItem)inventory.Items[inventory.EquipItemCount + choice - 1];

                inventory.ConsumeItem(selectedItem);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. ");
            }

            Sleep();
        }
    }
}
