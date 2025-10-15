using TextRPG.Enum;
using TextRPG.Item;

namespace TextRPG.Data
{
    internal class InventoryData
    {
        public List<ItemBase> Items { get; set; }

        public int EquipItemCount { get; set; }
        public int ConsumeItemCount { get; set; }

        // 장비 슬롯
        public Dictionary<EquipSlot, EquipItem> EquippedItems;
    }
}
