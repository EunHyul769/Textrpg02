using TextRPG.Enum;
using TextRPG.Item;

namespace TextRPG.Data
{
    internal class InventoryData
    {
        public List<ItemBase> items;

        public int equipItemCount;
        public int consumeItemCount;

        // 장비 슬롯
        public DictionaryConvertData equippedItems;
    }
}
