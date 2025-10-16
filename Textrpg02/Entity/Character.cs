using TextRPG.Data;
using TextRPG.Enum;
using TextRPG.Item;

namespace TextRPG.Entity
{
    internal class Character
    {
        // 생성자 주입
        public string Name { get; private set; }
        public int MaxHp { get; private set; }
        public int Hp { get; private set; }
        public int MaxMp { get; private set; }
        public int Mp { get; private set; }
        public int Attack { get; private set; }
        public int SkillAttack { get; private set; }
        public double AttackVariance { get; private set; } = 0.1; // 공격력에 ±10% 편차주기 위함
        public int Armor { get; private set; }
        public int MagicResistance { get; private set; }
        public JobType Job { get; private set; }

        public double CritChance { get; private set; } = 0.1;   //크리 확률 10퍼 고정

        public double CritMultiplier { get; private set; } = 1.5;   //크뎀 비율 150퍼 고정

        //public double AboidChance { get; private set; } = 0.05;     //회피 확률 5퍼 고정 임시해제. 나중에 이용.

        // 초기 세팅값
        public int Level { get; private set; }
        public int Gold { get; private set; }
        public int MaxExp { get; private set; }
        public int Exp { get; private set; }
        public int Stamina { get; private set; }
        public int MaxStamina { get; private set; } = 100;  //스태미너 최대치 100 고정

        // 아이템으로 얻은 추가 능력치
        public int BonusMaxHp { get; private set; }
        public int BonusMaxMp { get; private set; }
        public int BonusAttack { get; private set; }
        public int BonusSkillAttack { get; private set; }
        public int BonusArmor { get; private set; }
        public int BonusMagicResistance { get; private set; }

        // 인벤토리
        public Inventory Inventory { get; private set; }

        //습득한 스킬 ID목록
        public List<int> LearnedSkills { get; private set; }

        private static Random rng = new Random();

        public Character(string name, int maxHp, int maxMp, int attack, int skillAttack, int armor, int magicResistance, JobType job)
        {
            Name = name;
            MaxHp = maxHp;
            Hp = MaxHp;
            MaxMp = maxMp;
            Mp = MaxMp;
            Attack = attack;
            SkillAttack = skillAttack;
            Armor = armor;
            MagicResistance = magicResistance;
            Job = job;

            Level = 1;
            Gold = 5000;
            MaxExp = 100;
            Exp = 0;
            Stamina = 100;

            Inventory = new Inventory(this);

            LearnedSkills = new List<int>();    //배운 스킬 리스트
        }

        public void EquipItem(EquipItem item)
        {
            BonusMaxHp += item.BonusMaxHp;
            BonusMaxMp += item.BonusMaxMp;
            BonusAttack += item.BonusAttack;
            BonusSkillAttack += item.BonusSkillAttack;
            BonusArmor += item.BonusArmor;
            BonusMagicResistance += item.BonusMagicResistance;

            MaxHp += item.BonusMaxHp;
            Hp += item.BonusMaxHp;
            MaxMp += item.BonusMaxMp;
            Mp += item.BonusMaxMp;
            Attack += item.BonusAttack;
            SkillAttack += item.BonusSkillAttack;
            Armor += item.BonusArmor;
            MagicResistance += item.BonusMagicResistance;
        }

        public void UnequipItem(EquipItem item)
        {
            BonusMaxHp -= item.BonusMaxHp;
            BonusMaxMp -= item.BonusMaxMp;
            BonusAttack -= item.BonusAttack;
            BonusSkillAttack -= item.BonusSkillAttack;
            BonusArmor -= item.BonusArmor;
            BonusMagicResistance -= item.BonusMagicResistance;

            MaxHp -= item.BonusMaxHp;
            Hp -= item.BonusMaxHp;
            MaxMp -= item.BonusMaxMp;
            Mp -= item.BonusMaxMp;
            Attack -= item.BonusAttack;
            SkillAttack -= item.BonusSkillAttack;
            Armor -= item.BonusArmor;
            MagicResistance -= item.BonusMagicResistance;
        }

        public int AddHp(int hp)
        {
            Hp = Math.Min(MaxHp, Hp + hp);
            return Hp;
        }

        public int TakeHp(int hp)
        {
            Hp -= hp;
            if (Hp <= 0)
            {
                Hp = 0;
            }
            return Hp;
        }

        public int AddMp(int mp)
        {
            Mp = Math.Min(MaxMp, Hp + mp);
            return Mp;
        }

        public bool TakeMp(int mp)
        {
            if (Mp >= mp)
            {
                Mp -= mp;
                return true;
            }
            return false;
        }

        public int AddStamina(int stamina)
        {
            Stamina = Math.Min(MaxStamina, Stamina + stamina);
            return Stamina;
        }

        public bool TakeStamina(int stamina)
        {
            if (Stamina >= stamina)
            {
                Stamina -= stamina;
                return true;
            }
            return false;
        }

        public int AddGold(int gold)
        {
            Gold += gold;
            return Gold;
        }

        public bool TakeGold(int gold)
        {
            if (Gold >= gold)
            {
                Gold -= gold;
                return true;
            }
            return false;
        }

        public int AddExp(int exp)
        {
            Exp += exp;
            CheckExp();
            return Exp;
        }

        private void CheckExp()
        {
            while (Exp >= MaxExp)
            {
                AddExp(-MaxExp);
                LevelUp();
            }
        }

        private void LevelUp()
        {
            switch (Job)
            {
                case JobType.Warrior:
                    MaxHp += 20;
                    MaxMp += 4;
                    Attack += 3;
                    SkillAttack += 1;
                    Armor += 3;
                    MagicResistance += 3;
                    break;
                case JobType.Archer:
                    MaxHp += 16;
                    MaxMp += 8;
                    Attack += 2;
                    SkillAttack += 2;
                    Armor += 2;
                    MagicResistance += 2;
                    break;
                case JobType.Mage:
                    MaxHp += 12;
                    MaxMp += 12;
                    Attack += 1;
                    SkillAttack += 3;
                    Armor += 1;
                    MagicResistance += 1;
                    break;
            }

            Level++;
            Hp = MaxHp;
            Mp = MaxMp;
            MaxExp = (int)(MaxExp * 1.2f);

            Console.WriteLine($"레벨 업!");
            Console.WriteLine($"레벨이 {Level}로 상승했습니다.");
            Console.WriteLine($"Hp가 회복됩니다.\n");

            if (Level == 3) //3렙 달성시 직업별로 스킬 습득
            {

                if (Job == JobType.Warrior)
                {
                    LearnSkill(10);
                }
                else if (Job == JobType.Archer)
                {
                    LearnSkill(20);
                }
                else if (Job == JobType.Mage)
                {
                    LearnSkill(30);
                }
            }

            if (Level == 6) //6렙 달성시 직업별로 스킬 습득
            {

                if (Job == JobType.Warrior)
                {
                    LearnSkill(11);
                }
                else if (Job == JobType.Archer)
                {
                    LearnSkill(21);
                }
                else if (Job == JobType.Mage)
                {
                    LearnSkill(31);
                }
            }

            if (Level == 10) //10렙 달성시 직업별로 스킬 습득
            {

                if (Job == JobType.Warrior)
                {
                    LearnSkill(12);
                }
                else if (Job == JobType.Archer)
                {
                    LearnSkill(22);
                }
                else if (Job == JobType.Mage)
                {
                    LearnSkill(32);
                }
            }
        }

        //스킬 습득
        public void LearnSkill(int skillId)
        {
            if (!LearnedSkills.Contains(skillId))
            { 
                LearnedSkills.Add(skillId);
                Console.WriteLine($"{Name}이(가) {Data.SkillDB.Skills[skillId].Name} 스킬을 익혔습니다!");
                Console.ReadLine();
            }
        }

        //데미지 계산에 이용되는 실제 공격력(변동)
        public int GetRandomizedAttack()
        {
            double variance = 1 + rng.NextDouble() * (AttackVariance * 2) - AttackVariance;
            return (int)Math.Round(Attack * variance);
        }
        public int GetRandomizedSkillAttack()
        {
            double variance = 1 + rng.NextDouble() * (AttackVariance * 2) - AttackVariance;
            return (int)Math.Round(SkillAttack * variance);
        }
        //명시된 공격력의 90%~110% 사이 공격력이 랜덤하게 반환됨


        public string DisplayInfo()
        {
            string s = string.Empty;
            s += $"닉네임: {Name}\n";
            s += $"Lv. {Level}\n";
            s += $"직업: {Job}\n";
            s += $"HP: {Hp} / {MaxHp} {(BonusMaxHp != 0 ? $"(+{BonusMaxHp})" : "")}\n";
            s += $"MP: {Mp} / {MaxMp} {(BonusMaxMp != 0 ? $"(+{BonusMaxMp})" : "")}\n";
            s += $"공격력: {Attack} {(BonusAttack != 0 ? $"(+{BonusAttack})" : "")}\n";
            s += $"주문력: {SkillAttack} {(BonusSkillAttack != 0 ? $"(+{BonusSkillAttack})" : "")}\n";
            s += $"방어력: {Armor + BonusArmor} {(BonusArmor != 0 ? $"(+{BonusArmor})" : "")}\n";
            s += $"마법저항력: {MagicResistance + BonusMagicResistance} {(BonusMagicResistance != 0 ? $"(+{BonusMagicResistance})" : "")}\n";
            s += $"Gold: {Gold} G\n";
            s += $"Exp: {Exp} / {MaxExp}\n";
            s += $"스테미나: {Stamina}\n";
            return s;
        }

        public void SetInventory(Inventory inventory)
        {
            Inventory = inventory;
        }

        public static Character LoadData(CharacterData data)
        {
            Character character = new Character(data.name, data.maxHp, data.maxMp, data.attack, data.skillAttack, data.armor, data.magicResistance, data.job);

            character.Level = data.level;
            character.Gold = data.gold;
            character.MaxExp = data.maxExp;
            character.Exp = data.exp;
            character.Stamina = data.stamina;

            character.BonusMaxHp = data.bonusMaxHp;
            character.BonusMaxMp = data.bonusMaxMp;
            character.BonusAttack = data.bonusAttack;
            character.BonusSkillAttack = data.bonusSkillAttack;
            character.BonusArmor = data.bonusArmor;
            character.BonusMagicResistance = data.bonusMagicResistance;

            //배운 스킬 있으면 스킬도 불러오기
            if (data.learnedSkills != null)
                character.LearnedSkills = new List<int>(data.learnedSkills);

            return character;
        }
    }
}
