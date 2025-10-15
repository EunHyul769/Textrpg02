using TextRPG.Data;
using TextRPG.Enum;
using TextRPG.Item;

namespace TextRPG.Entity
{
    internal class Inventory
    {
        public List<ItemBase> Items { get; private set; } = new List<ItemBase>();

        public int EquipItemCount { get; private set; }
        public int ConsumeItemCount { get; private set; }

        // 장비 슬롯
        public Dictionary<EquipSlot, EquipItem> EquippedItems { get; private set; } = new Dictionary<EquipSlot, EquipItem>();

        private Character character;

        public Inventory()
        {
            foreach (EquipSlot slot in System.Enum.GetValues(typeof(EquipSlot)))
            {
                EquippedItems.Add(slot, null);
            }
        }

        public Inventory(Character character)
        {
            this.character = character;

            foreach (EquipSlot slot in System.Enum.GetValues(typeof(EquipSlot)))
            {
                EquippedItems.Add(slot, null);
            }
        }

        // 인벤토리에 아이템 추가
        public void AddItem(ItemBase item)
        {
            if (item is EquipItem equipItem)
            {
                EquipItemCount++;
                equipItem.IsBuy = true;
            }
            if (item is ConsumeItem)
                ConsumeItemCount++;
            Items.Add(item);
            Items.Sort((itemA, itemB) =>
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
        }

        // 아이템 판매
        public void SellItem(ItemBase item)
        {
            if (item is EquipItem equipItem)
            {
                if (equipItem.IsEquipped)
                    EquipItem(equipItem);
                equipItem.IsBuy = false;
            }
            RemoveItem(item);
        }

        // 인벤토리에서 아이템 제거
        private void RemoveItem(ItemBase item)
        {
            if (item is EquipItem)
                EquipItemCount--;
            else
                ConsumeItemCount--;
            Items.Remove(item);
        }

        public void EquipItem(EquipItem item)
        {
            // 슬롯에 아이템을 장착 중이면 해제
            if (EquippedItems[item.EquipSlot] != null)
            {
                EquippedItems[item.EquipSlot].IsEquipped = false;
                character.UnequipItem(EquippedItems[item.EquipSlot]);
            }

            // 같은 아이템이면 해제
            if (EquippedItems[item.EquipSlot] == item)
            {
                EquippedItems[item.EquipSlot].IsEquipped = false;
                EquippedItems[item.EquipSlot] = null;
            }
            // 다른 아이템이면 변경
            else
            {
                EquippedItems[item.EquipSlot] = item;
                EquippedItems[item.EquipSlot].IsEquipped = true;
                character.EquipItem(item);
            }
        }

        public void ConsumeItem(ConsumeItem item)
        {
            item.Use(character);
            RemoveItem(item);
        }

        public void SetCharacter(Character character)
        {
            this.character = character;
        }

        public static Inventory LoadData(InventoryData inventoryData, Dictionary<EquipSlot, EquipItem> restoredEquipped)
        {
            Inventory inventory = new Inventory();
            inventory.Items = inventoryData.items;
            inventory.EquipItemCount = inventoryData.equipItemCount;
            inventory.ConsumeItemCount = inventoryData.consumeItemCount;
            inventory.EquippedItems = restoredEquipped;

            return inventory;
        }
    }
}
