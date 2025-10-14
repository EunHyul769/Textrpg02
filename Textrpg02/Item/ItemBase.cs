
namespace TextRPG.Item
{
    internal abstract class ItemBase
    {
        // 모든 아이템의 공통 속성
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Price { get; private set; }

        public ItemBase(string name, string description, int price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public abstract string DisplayInfo();
    }
}
