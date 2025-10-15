using TextRPG.Item;

namespace TextRPG.Data.DB
{
    internal class ConsumeItemDB
    {
        public Dictionary<int, ConsumeItem> Items { get; private set; } = new Dictionary<int, ConsumeItem>()
        {
            { 1000, new ConsumeItem("빨간 포션", "Hp를 50 회복시켜줍니다.", 1000, 50, 0) },
            { 1001, new ConsumeItem("파란 포션", "Mp를 50 회복시켜줍니다.", 1000, 0, 50) },
            { 1002, new ConsumeItem("엘릭서", "Hp, Mp를 모두 50 회복시켜줍니다.", 2000, 50, 50) },
        };

        public ConsumeItem? GetByKey(int key)
        {
            if (Items.ContainsKey(key)) 
                return Items[key].Clone() as ConsumeItem;
            return null;
        }
    }
}
