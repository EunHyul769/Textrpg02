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
                    // 스킬 공격 (Skill.CalculateSkillDamage 호출)
                    damage = SkillDamage(c, defender, skill);
                }
            }
            else if (attacker is Monster m)
            {
                if (skill == null)
                    damage = CalculateBasicDamage(m.Atk, GetArmor(defender), null);
                else
                    damage = SkillDamage(m, defender, skill);
            }

            return Math.Max(1, (int)Math.Round(damage));
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

        // 🧙 스킬 공격 — Skill.CalculateSkillDamage() 활용
        private static double SkillDamage(object attacker, object defender, Skill skill)
        {
            double baseDamage = 0;
            double defenseValue = 0;

            // (1) Skill.cs의 계산 메서드를 그대로 호출
            if (attacker is Character c)
            {
                baseDamage = skill.CalculateSkillDamage(c);
            }
            else if (attacker is Monster m)
            {
                // 몬스터는 단일 공격력 기준으로만 계산
                baseDamage = m.Atk * (skill.Power + skill.SPower);
            }

            // (2) 스킬 타입 판별: SPower가 크면 마법형
            bool isMagical = skill.SPower > skill.Power;

            // (3) 방어 계산
            defenseValue = defender switch
            {
                Character charDef => isMagical
                    ? charDef.MagicResistance + charDef.BonusMagicResistance
                    : charDef.Armor + charDef.BonusArmor,

                Monster monDef => monDef.Def, // 몬스터는 단일 방어
                _ => 0
            };

            // (4) 방어력 반영
            double finalDamage = Math.Max(1, baseDamage - (defenseValue / 2.0));

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{GetName(attacker)}이(가) {skill.Name}을(를) 사용했다!");
            Console.ResetColor();
            return finalDamage;
        }

        // 방어 계산 공용
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

