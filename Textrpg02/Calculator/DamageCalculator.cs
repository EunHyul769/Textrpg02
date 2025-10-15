using TextRPG;
using TextRPG.Entity; // Player, Monster 클래스를 사용하기 위함 물론, 스킬도

namespace TextRPG.Calculator
{
    internal class DamageCalculator
    {
        private static readonly Random rng = new Random();

        // 🎯 진입점: 모든 공격 계산
        public static int CalculateAttack(object attacker, object defender, Skill? skill = null)
        {
            if (TryEvade(defender))
            {
                Log($"{GetName(defender)}이(가) 재빠르게 공격을 회피했다!", ConsoleColor.Cyan);
                return 0;
            }

            double damage = skill == null
                ? CalculateBasicAttack(attacker, defender)
                : CalculateSkillAttack(attacker, defender, skill);

            return Math.Max(1, (int)Math.Round(damage));
        }

        // ⚔️ 기본 공격
        private static double CalculateBasicAttack(object attacker, object defender)
        {
            if (attacker is Character c)
            {
                int atk = c.Attack + c.BonusAttack;
                int def = GetDefenseValue(defender, null);
                return ApplyCritical(Math.Max(1, atk - (def / 2.0)), c);
            }
            else if (attacker is Monster m)
            {
                int atk = m.Atk;
                int def = GetDefenseValue(defender, null);
                return Math.Max(1, atk - (def / 2.0));
            }

            return 0;
        }

        // 🔥 스킬 공격 (다단히트/단일히트 모두 대응)
        private static double CalculateSkillAttack(object attacker, object defender, Skill skill)
        {
            double totalDamage = 0;
            bool isMagical = skill.SPower > skill.Power;

            // 1️⃣ 공격자에 따라 스킬 데미지 가져오기
            if (attacker is Character c)
            {
                var hitDamages = skill.CalculateSkillDamage(c);

                foreach (var hit in hitDamages)
                {
                    double def = GetDefenseValue(defender, skill);
                    double final = ApplyDefense(hit, def);
                    totalDamage += final;

                    Log($"{c.Name}의 {skill.Name} ▶ {final:F0} 데미지!", ConsoleColor.Magenta);
                }
            }
            else if (attacker is Monster m)
            {
                double baseDamage = m.Atk * (skill.Power + skill.SPower);
                double def = GetDefenseValue(defender, skill);
                double final = ApplyDefense(baseDamage, def);
                totalDamage = final;

                Log($"{m.Name}이(가) {skill.Name}을(를) 사용했다! ▶ {final:F0}", ConsoleColor.Red);
            }

            return totalDamage;
        }

        // 🧱 방어 계산
        private static int GetDefenseValue(object defender, Skill? skill)
        {
            if (defender is Character c)
            {
                if (skill == null)
                    return c.Armor + c.BonusArmor;

                bool isMagic = skill.SPower > skill.Power;
                return isMagic
                    ? c.MagicResistance + c.BonusMagicResistance
                    : c.Armor + c.BonusArmor;
            }

            if (defender is Monster m)
                return m.Def;

            return 0;
        }

        // 💥 회피 (5%)
        private static bool TryEvade(object defender) => rng.NextDouble() < 0.05;

        // 💫 크리티컬 (기본 공격 전용)
        private static double ApplyCritical(double baseDamage, Character c)
        {
            if (rng.NextDouble() < c.CritChance)
            {
                Log("★ 크리티컬 히트! ★", ConsoleColor.Yellow);
                return baseDamage * c.CritMultiplier;
            }

            return baseDamage;
        }

        // 🛡️ 방어력 적용 (모든 공격)
        private static double ApplyDefense(double baseDamage, double defenseValue)
        {
            return Math.Max(1, baseDamage - (defenseValue / 2.0));
        }

        // 🔊 출력 (색상 + 자동 복구)
        private static void Log(string message, ConsoleColor color)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = prev;
        }

        // 🧩 이름 추출
        private static string GetName(object entity)
        {
            return entity switch
            {
                Character c => c.Name,
                Monster m => m.Name,
                _ => "알 수 없는 존재"
            };
        }
    }
}
//캐릭터cs에 CritChance, CritMultiplier 속성 추가

//일단은 몬스터도 스킬을 쓸 수 있게 설계

