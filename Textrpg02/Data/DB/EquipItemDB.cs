using TextRPG.Item;

namespace TextRPG.Data.DB
{
    internal class EquipItemDB
    {
        public Dictionary<int, EquipItem> Items { get; private set; } = new Dictionary<int, EquipItem>()
        {
            { 2000, new EquipItem("연습용 칼", "연습할 때 쓰던 칼", 1000, Enum.ItemType.Equip, Enum.JobType.Warrior, Enum.EquipSlot.Weapon, 0, 0, 10, 5, 0) },
            { 2001, new EquipItem("연습용 활", "연습할 때 쓰던 활", 1000, Enum.ItemType.Equip, Enum.JobType.Archer, Enum.EquipSlot.Weapon, 0, 0, 10, 5, 0) },
            { 2002, new EquipItem("연습용 지팡이", "연습할 때 쓰던 지팡이", 1000, Enum.ItemType.Equip, Enum.JobType.Mage, Enum.EquipSlot.Weapon, 0, 0, 10, 5, 0) },

            { 3000, new EquipItem("일반 판금 갑옷", "철로 만든 갑옷", 1000, Enum.ItemType.Equip, Enum.JobType.Warrior, Enum.EquipSlot.Armor, 50, 10, 0, 0, 5, 5) },
            { 3001, new EquipItem("고급 판금 갑옷", "강철로 만든 갑옷", 1000, Enum.ItemType.Equip, Enum.JobType.Warrior, Enum.EquipSlot.Armor, 100, 20, 0, 0, 10, 10) },
            { 3002, new EquipItem("일반 가죽 갑옷", "가죽으로 만든 갑옷", 1000, Enum.ItemType.Equip, Enum.JobType.Archer, Enum.EquipSlot.Armor, 50, 10, 0, 0, 5, 5) },
            { 3003, new EquipItem("고급 가죽 갑옷", "고급 가죽으로 만든 갑옷", 1000, Enum.ItemType.Equip, Enum.JobType.Archer, Enum.EquipSlot.Armor, 100, 20, 0, 0, 10, 10) },
            { 3004, new EquipItem("일반 천 갑옷", "천으로 만든 갑옷", 1000, Enum.ItemType.Equip, Enum.JobType.Mage, Enum.EquipSlot.Armor, 50, 10, 0, 0, 5, 5) },
            { 3005, new EquipItem("고급 천 갑옷", "고급 천으로 만든 갑옷", 1000, Enum.ItemType.Equip, Enum.JobType.Mage, Enum.EquipSlot.Armor, 100, 20, 0, 0, 10, 10) },
        };

        public EquipItem? GetByKey(int key)
        {
            if (Items.ContainsKey(key))
                return Items[key].Clone() as EquipItem;
            return null;
        }
    }
}
