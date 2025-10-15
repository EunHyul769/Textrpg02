using TextRPG.Entity;

namespace TextRPG.Item
{
    internal class ConsumeItem : ItemBase
    {
        public int RecoverHP { get; private set; }
        public int RecoverMP { get; private set; }

        public ConsumeItem(string name, string description, int price, int recoverHP, int recoverMP) : base(name, description, price)
        {
            RecoverHP = recoverHP;
            RecoverMP = recoverMP;
        }

        public void Use(Character character)
        {
            character.AddHp(RecoverHP);
            character.AddMp(RecoverMP);
        }

        public override string DisplayInfo()
        {
            return $"{Name} | {Description}{EffectInfo()}";
        }

        public string EffectInfo()
        {
            string s = string.Empty;
            if (RecoverHP > 0) s += $" | 체력 회복 +{RecoverHP}";
            if (RecoverMP > 0) s += $" | 마나 회복 +{RecoverMP}";
            return s;
        }

        public override ItemBase Clone()
        {
            return new ConsumeItem(Name, Description, Price, RecoverHP, RecoverMP);
        }
    }
}
