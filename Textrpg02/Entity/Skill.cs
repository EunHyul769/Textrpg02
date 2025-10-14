
namespace TextRPG.Entity
{
    internal class Skill
    {
        public string Name { get; set; }
        public double Power { get; set; }  // 물딜 배율
        public double SPower { get; set; }  //마딜 배율

        public int MpCost { get; set; }

        public Skill(string name, double power, double spower, int mpCost)
        {
            Name = name;
            Power = power;
            SPower = spower;
            MpCost = mpCost;
        }

        // atk*배율 + spd*배율 + luk*배율 + def*배율 + 고정치?
        public double CalculateAttackDamage(Player player)
        {
            return player.Attack * Power;
        }

        public double CalculateSkillAttackDamage(Player player)
        {
            return player.SkillAttack * SPower;
        }
    }

}
