using Newtonsoft.Json;

namespace TextRPG.Item
{
    internal abstract class ItemBase
    {
        // 모든 아이템의 공통 속성
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Price { get; private set; }

        public ItemBase(int id, string name, string description, int price)
        {
            ID = id;
            Name = name;
            Description = description;
            Price = price;
        }

        public abstract string DisplayInfo();

        public abstract ItemBase Clone();
    }
}
