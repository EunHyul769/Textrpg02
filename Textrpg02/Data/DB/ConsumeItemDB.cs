using TextRPG.Item;

namespace TextRPG.Data.DB
{
    internal class ConsumeItemDB
    {
        public Dictionary<int, ConsumeItem> Items { get; private set; } = new Dictionary<int, ConsumeItem>()
        {
            { 1, new ConsumeItem("소비 아이템", "체력을 50 회복", 1000, 50, 0) },
        };

        public ConsumeItem? GetByKey(int key)
        {
            if (Items.ContainsKey(key)) 
                return Items[key].Clone() as ConsumeItem;
            return null;
        }
    }
}
