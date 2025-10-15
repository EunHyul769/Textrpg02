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
                int totalAtk = c.Attack + c.BonusAttack;
                int totalSkillAtk = c.SkillAttack + c.BonusSkillAttack;

                if (skill == null)
                {
                    // 기본 공격 (크리티컬 가능)
                    damage = CalculateBasicDamage(totalAtk, GetArmor(defender), c);
                }
                else
                {
                    // 스킬 공격 (물리/마법 판별)
                    damage = CalculateSkillDamage(c, defender, skill, totalAtk, totalSkillAtk);
                }
            }
            else if (attacker is Monster m)
            {
                if (skill == null)
                    damage = CalculateBasicDamage(m.Atk, GetArmor(defender), null);
                else
                    damage = CalculateSkillDamage(m, defender, skill, m.Atk, 0);
            }

            return Math.Max(1, (int)Math.Round(damage));
        }

        // ⚔️ 기본 공격 (크리티컬 적용)
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

        // 🔥 스킬 공격 (Skill.cs 수정 없이, 몬스터 방어 통합)
        private static double CalculateSkillDamage(object attacker, object defender, Skill skill, int atk, int skillAtk)
        {
            double baseDamage = 0;
            double defenseValue = 0;

            // (1) 물리/마법 데미지 계산
            if (attacker is Character c)
            {
                baseDamage = (atk * skill.Power) + (skillAtk * skill.SPower);
            }
            else if (attacker is Monster m)
            {
                baseDamage = m.Atk * skill.Power;
            }

            // (2) 스킬 타입 판별 — SPower이 더 크면 마법형
            bool isMagical = skill.SPower > skill.Power;

            // (3) 방어 계산
            defenseValue = defender switch
            {
                Character charDef => isMagical
                    ? charDef.MagicResistance + charDef.BonusMagicResistance
                    : charDef.Armor + charDef.BonusArmor,

                Monster monDef => monDef.Def, // 몬스터는 단일 Def로 통일
                _ => 0
            };

            // (4) 최종 데미지
            double finalDamage = Math.Max(1, baseDamage - (defenseValue / 2.0));

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{GetName(attacker)}이(가) {skill.Name}을(를) 사용했다!");
            Console.ResetColor();
            return finalDamage;
        }

        // 🛡️ 방어력 계산 (기본 공격용)
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

