using TextRPG.Item;

namespace TextRPG.Entity
{
    internal class Shop
    {
        public List<ItemBase> Items { get; private set; } = new List<ItemBase>();

        public int EquipItemCount { get; private set; }
        public int ConsumeItemCount { get; private set; }

        public Shop()
        {
            // 테스트용 아이템 추가
            AddItem(new EquipItem("전사 무기1", "전사 무기1...", 1000, Enum.ItemType.Equip, Enum.JobType.Warrior, Enum.EquipSlot.Weapon, 0, 0, 10, 5, 0));
            AddItem(new EquipItem("궁수 무기1", "궁수 무기1...", 1000, Enum.ItemType.Equip, Enum.JobType.Archer, Enum.EquipSlot.Weapon, 0, 0, 10, 5, 0));
            AddItem(new EquipItem("법사 무기1", "법사 무기1...", 1000, Enum.ItemType.Equip, Enum.JobType.Mage, Enum.EquipSlot.Weapon, 0, 0, 10, 5, 0));

            AddItem(new EquipItem("전사 방어구1", "전사 방어구1...", 1000, Enum.ItemType.Equip, Enum.JobType.Warrior, Enum.EquipSlot.Armor, 50, 10, 0, 0, 5, 5));
            AddItem(new EquipItem("전사 방어구2", "전사 방어구2...", 1000, Enum.ItemType.Equip, Enum.JobType.Warrior, Enum.EquipSlot.Armor, 100, 20, 0, 0, 10, 10));
            AddItem(new EquipItem("궁수 방어구1", "궁수 방어구1...", 1000, Enum.ItemType.Equip, Enum.JobType.Archer, Enum.EquipSlot.Armor, 50, 10, 0, 0, 5, 5));
            AddItem(new EquipItem("궁수 방어구2", "궁수 방어구2...", 1000, Enum.ItemType.Equip, Enum.JobType.Archer, Enum.EquipSlot.Armor, 100, 20, 0, 0, 10, 10));
            AddItem(new EquipItem("법사 방어구1", "법사 방어구1...", 1000, Enum.ItemType.Equip, Enum.JobType.Mage, Enum.EquipSlot.Armor, 50, 10, 0, 0, 5, 5));
            AddItem(new EquipItem("법사 방어구2", "법사 방어구2...", 1000, Enum.ItemType.Equip, Enum.JobType.Mage, Enum.EquipSlot.Armor, 100, 20, 0, 0, 10, 10));

            AddItem(new ConsumeItem("소비 아이템1", "Hp를 50 회복시켜줍니다.", 1000, 50, 0));
            AddItem(new ConsumeItem("소비 아이템2", "Mp를 50 회복시켜줍니다.", 1000, 0, 50));
            AddItem(new ConsumeItem("소비 아이템3", "Hp, Mp를 모두 50 회복시켜줍니다.", 2000, 50, 50));
        }

        // 상점에 아이템 추가
        public void AddItem(ItemBase item)
        {
            if (item is EquipItem)
                EquipItemCount++;
            if (item is ConsumeItem)
                ConsumeItemCount++;
            Items.Add(item);
        }
    }
}
