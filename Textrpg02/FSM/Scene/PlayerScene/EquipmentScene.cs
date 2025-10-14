using TextRPG.Entity;
using TextRPG.Item;

namespace TextRPG.FSM.Scene.PlayerScene
{
    internal class EquipmentScene : SceneBase
    {
        private Character character;
        private Inventory inventory;

        public EquipmentScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "인벤토리 - 장비 아이템 관리";
            character = GameManager.Instance.Character;
            inventory = GameManager.Instance.Character.Inventory;
        }

        protected override void View()
        {
            Console.WriteLine("[인벤 - 장비 아이템]\n");
            for (int i = 0; i < inventory.Items.Count; i++)
            { 
                if (inventory.Items[i] is EquipItem item)
                {
                    string isEquipped = item.IsEquipped ? " [E]" : "";
                    Console.WriteLine($"- {i + 1} {isEquipped} {item.DisplayInfo()}");
                }
            }
            Console.WriteLine();

            Console.WriteLine("0. 인벤토리로 돌아가기\n");
        }

        protected override void Control()
        {
            Console.Write("장착/해제할 아이템 번호를 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                controller.ChangeSceneState(controller.InventoryScene);
                return;
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= inventory.EquipItemCount)
            {
                // 유효한 인덱스 선택 (배열 인덱스는 0부터 시작하므로 -1)
                EquipItem selectedItem = (EquipItem)inventory.Items[choice - 1];

                // 직업이 맞으면 장비 장착/해제
                if (selectedItem.CanEquip(character.Job))
                    inventory.EquipItem(selectedItem);
                else
                    Console.WriteLine($"{character.Job} 은(는) 착용할 수 없는 장비입니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. ");
            }

            Sleep();
        }
    }
}
