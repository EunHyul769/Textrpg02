using TextRPG.Item;

namespace TextRPG.Data.DB
{
    internal class EquipItemDB
    {
        public Dictionary<int, EquipItem> Items { get; private set; } = new Dictionary<int, EquipItem>()
        {
            { 1, new EquipItem("전사 무기1", "전사 무기1...", 1000, Enum.ItemType.Equip, Enum.JobType.Warrior, Enum.EquipSlot.Weapon, 0, 0, 10, 5, 0) },
        };

        public EquipItem? GetByKey(int key)
        {
            if (Items.ContainsKey(key))
                return Items[key].Clone() as EquipItem;
            return null;
        }
    }
}
