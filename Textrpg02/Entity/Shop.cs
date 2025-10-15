using TextRPG.Data;
using TextRPG.Item;
using TextRPG.Manager;

namespace TextRPG.Entity
{
    internal class Shop
    {
        public List<ItemBase> Items { get; private set; } = new List<ItemBase>();

        public int EquipItemCount { get; private set; }
        public int ConsumeItemCount { get; private set; }

        private readonly GameData gameData;

        public Shop()
        {
            gameData = DataManager.Instance.GameData;
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

        public void AddItem(int index)
        {
            ItemBase item = gameData.ConsumeItemDB.GetByKey(index);
            if (item == null)
                item = gameData.EquipItemDB.GetByKey(index);
            if (item != null)
            {
                Items.Add(item);
                if (item is EquipItem)
                    EquipItemCount++;
                else
                    ConsumeItemCount++;
            }
        }
    }
}
