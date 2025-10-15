using TextRPG;
using TextRPG.Entity; // Player, Monster 클래스를 사용하기 위함 물론, 스킬도

namespace TextRPG.Calculator
{
    internal class DamageCalculator
    {
        private static Random rng = new Random();

        public static int CalculateAttack(object attacker, object defender, Skill? skill = null)
        {
            double damage = 0.0;

            // 🎯 1️⃣ 공격 전 회피 판정
            if (TryEvade(defender))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{GetName(defender)}이(가) 재빠르게 공격을 회피했다!");
                Console.ResetColor();
                return 0;
            }

            if (attacker is Character c)
            {
                if (skill == null)
                {
                    // 기본 공격 (크리티컬 가능)
                    damage = CalculateBasicDamage(
                        c.Attack + c.BonusAttack,
                        GetArmor(defender),
                        c
                    );
                }
                else
                {
                    // 스킬 공격
                    damage = CalculateSkillDamage(c, defender, skill);
                }
            }
            else if (attacker is Monster m)
            {
                if (skill == null)
                    damage = CalculateBasicDamage(m.Atk, GetArmor(defender), null);
                else
                    damage = CalculateSkillDamage(m, defender, skill);
            }

            return Math.Max(1, (int)Math.Round(damage));
        }

        // ⚡ 2️⃣ 회피 판정
        private static bool TryEvade(object defender)
        {
            double evasionChance = 0.05; // 5%
            return rng.NextDouble() < evasionChance;
        }

        // ⚔️ 기본 공격 (크리티컬 가능)
        private static double CalculateBasicDamage(int atk, int def, Character? character)
        {
            int baseDamage = Math.Max(1, atk - (def / 2));

            if (character != null)
            {
                bool isCrit = rng.NextDouble() < character.CritChance;
                if (isCrit)
                {
                    baseDamage = (int)Math.Round(baseDamage * character.CritMultiplier);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("★ 크리티컬 히트! ★");
                    Console.ResetColor();
                }
            }

            return baseDamage;
        }

        // 🧙 스킬 공격 (Skill.CalculateSkillDamage 호출)
        private static double CalculateSkillDamage(object attacker, object defender, Skill skill)
        {
            double baseDamage = 0;
            double defenseValue = 0;
            double totalDamage = 0; // ← 다단히트 누적용

            // (1) Skill.cs 계산기 호출
            if (attacker is Character c)
            {
                // skill.CalculateSkillDamage()가 여러 히트라면 IEnumerable<double> 또는 List<double> 반환
                var damages = skill.CalculateSkillDamage(c);

                // 단일 히트 스킬이면 foreach에서 한 번만 돈다.
                foreach (var hitDamage in damages)
                {
                    baseDamage = hitDamage;

                    // (2) 스킬 타입 판별
                    bool isMagical = skill.SPower > skill.Power;

                    // (3) 방어 계산
                    defenseValue = defender switch
                    {
                        Character charDef => isMagical
                            ? charDef.MagicResistance + charDef.BonusMagicResistance
                            : charDef.Armor + charDef.BonusArmor,

                        Monster monDef => monDef.Def,
                        _ => 0
                    };

                    // (4) 방어력 적용
                    double finalDamage = Math.Max(1, baseDamage - (defenseValue / 2.0));
                    totalDamage += finalDamage;

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{GetName(attacker)}의 {skill.Name} [{Math.Round(finalDamage)} 데미지!]");
                    Console.ResetColor();
                }
            }
            else if (attacker is Monster m)
            {
                baseDamage = m.Atk * (skill.Power + skill.SPower);

                bool isMagical = skill.SPower > skill.Power;
                defenseValue = defender switch
                {
                    Character charDef => isMagical
                        ? charDef.MagicResistance + charDef.BonusMagicResistance
                        : charDef.Armor + charDef.BonusArmor,
                    Monster monDef => monDef.Def,
                    _ => 0
                };

                double finalDamage = Math.Max(1, baseDamage - (defenseValue / 2.0));
                totalDamage += finalDamage;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{GetName(attacker)}이(가) {skill.Name}을(를) 사용했다! [{Math.Round(finalDamage)} 데미지]");
                Console.ResetColor();
            }

            return totalDamage; // ← 다단히트면 총합 반환, 단일히트면 기존과 동일
        }

        private static int GetArmor(object defender)
        {
            if (defender is Character c)
                return c.Armor + c.BonusArmor;
            if (defender is Monster m)
                return m.Def;
            return 0;
        }

        private static string GetName(object entity)
        {
            if (entity is Character c) return c.Name;
            if (entity is Monster m) return m.Name;
            return "알 수 없는 존재";
        }
    }
}
//캐릭터cs에 CritChance, CritMultiplier 속성 추가

//일단은 몬스터도 스킬을 쓸 수 있게 설계

