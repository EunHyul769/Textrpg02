using Newtonsoft.Json;
using TextRPG.Enum;

namespace TextRPG.Item
{
    internal class EquipItem : ItemBase
    {
        public ItemType Type { get; private set; }
        public JobType PermitJob { get; private set; }
        public EquipSlot EquipSlot;

        // 능력치 보너스 필드
        public int BonusMaxHp { get; private set; }
        public int BonusMaxMp { get; private set; }
        public int BonusAttack { get; private set; }
        public int BonusSkillAttack { get; private set; }
        public int BonusArmor { get; private set; }
        public int BonusMagicResistance { get; private set; }

        public bool IsEquipped { get; set; } = false;
        public bool IsBuy { get; set; } = false;


        public EquipItem(int id, string name, string description, int price, ItemType type, JobType permitJob, EquipSlot slot, int bonusMaxHp = 0, int bonusMaxMp = 0, int bonusAttack = 0, int bonusSkillAttack = 0, int bonusArmor = 0, int bonusMagicResistance = 0)
            : base(id, name, description, price)
        {
            // 장비 타입만 받도록 제한
            if (type == ItemType.Equip)
            {
                Type = type;
                PermitJob = permitJob;
                EquipSlot = slot;

                BonusMaxHp = bonusMaxHp;
                BonusMaxMp = bonusMaxMp;
                BonusAttack = bonusAttack;
                BonusSkillAttack = bonusSkillAttack;
                BonusArmor = bonusArmor;
                BonusMagicResistance = bonusMagicResistance;
            }
            else
            {
                Console.WriteLine("장비 아이템은 타입에 장비만 넣을 수 있습니다.");
            }
        }

        public bool CanEquip(JobType characterJob)
        {
            return PermitJob == characterJob;
        }

        public override string DisplayInfo()
        {
            return $"{Name} | {Description}{StatInfo()}";
        }

        public string StatInfo()
        {
            string s = string.Empty;
            if (BonusMaxHp > 0) s += $" | 체력 +{BonusMaxHp}";
            if (BonusMaxMp > 0) s += $" | 마나 +{BonusMaxMp}";
            if (BonusAttack > 0) s += $" | 공격력 +{BonusAttack}";
            if (BonusSkillAttack > 0) s += $" | 주문력 +{BonusSkillAttack}";
            if (BonusArmor > 0) s += $" | 방어력 +{BonusArmor}";
            if (BonusMagicResistance > 0) s += $" | 마법저항력 +{BonusMagicResistance}";
            return s;
        }

        public override ItemBase Clone()
        {
            return new EquipItem(ID, Name, Description, Price, Type, PermitJob, EquipSlot, BonusMaxHp, BonusMaxMp, BonusAttack, BonusSkillAttack, BonusArmor, BonusMagicResistance);
        }
    }
}
