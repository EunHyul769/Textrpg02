using TextRPG.Entity;
using TextRPG.SkillSystem;

namespace TextRPG.Calculator
{
    internal class DamageCalculator
    {
        private static readonly Random rng = new Random();

        //  모든 공격 계산
        public static int CalculateAttack(object attacker, object defender, Skill? skill = null)
        {
            // 회피 판정
            if (TryEvade(defender))
            {
                Log($"{GetName(defender)}이(가) 재빠르게 공격을 회피했다!", ConsoleColor.Cyan);
                return 0;
            }

            // 기본 공격
            if (skill == null)
            {
                double damage = CalculateBasicAttack(attacker, defender);
                return Math.Max(1, (int)Math.Round(damage));
            }

            // 스킬 공격 (다단히트 등 내부 처리)
            else
            {
                CalculateSkillAttack(attacker, defender, skill);  // void 함수
                return 0; // 스킬은 즉시 적용되므로 반환값은 의미 없음
            }
        }


        // 기본 공격
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

        // 스킬 공격 (단일/다중 히트 모두 처리)
        private static void CalculateSkillAttack(object attacker, object defender, Skill skill)
        {
            bool isMagical = skill.SPower > skill.Power;

            if (attacker is Character c)
            {
                var hitDamages = skill.CalculateSkillDamage(c);

                foreach (var hit in hitDamages)
                {
                    double def = GetDefenseValue(defender, skill);
                    double final = ApplyDefense(hit, def);
                    int damageInt = (int)Math.Round(final);

                    if (defender is Monster m)
                    {
                        int beforeHp = m.Hp; // 감소 전 HP
                        int afterHp = Math.Max(0, m.Hp - damageInt); // 감소 후 HP

                        Log($"{c.Name}의 {skill.Name} - {final:F0} 피해!", ConsoleColor.Red);
                        Log($"몬스터의 HP {beforeHp} → {afterHp}", ConsoleColor.Red);

                        Thread.Sleep(400);

                        m.Hp = afterHp; // 실제 HP 적용
                    }
                    else if (defender is Character target)
                    {
                        int expectedHp = Math.Max(0, target.Hp - damageInt);
                        Log($"{c.Name}의 {skill.Name} - {final:F0} 데미지! (플레이어 HP: {expectedHp})", ConsoleColor.Red);

                        target.TakeHp(damageInt);
                    }

                    // 히트 간 템포 (0.3초)
                    System.Threading.Thread.Sleep(300);
                }
            }
            else if (attacker is Monster mon)
            {
                double baseDamage = mon.Atk * (skill.Power + skill.SPower);
                double def = GetDefenseValue(defender, skill);
                double final = ApplyDefense(baseDamage, def);
                int damageInt = (int)Math.Round(final);

                if (defender is Character target)
                {
                    int expectedHp = Math.Max(0, target.Hp - damageInt);
                    Log($"{mon.Name}이(가) {skill.Name}을(를) 사용했다! ▶ {final:F0} (플레이어 HP: {expectedHp})", ConsoleColor.Red);

                    target.TakeHp(damageInt);
                }
            }
        }


        // 방어력 값 추출 (기본/스킬 공격 공통)
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


        // 회피 (5%)
        private static bool TryEvade(object defender) => rng.NextDouble() < 0.05;

        // 크리티컬 (기본 공격 전용)
        private static double ApplyCritical(double baseDamage, Character c)
        {
            if (rng.NextDouble() < c.CritChance)
            {
                Log("크리티컬 히트!", ConsoleColor.Yellow);
                return baseDamage * c.CritMultiplier;
            }

            return baseDamage;
        }

        // 방어력 적용 (모든 공격)
        private static double ApplyDefense(double baseDamage, double defenseValue)
        {
            return Math.Max(1, baseDamage - (defenseValue / 2.0));
        }

        // 출력 (색상 + 자동 복구)
        private static void Log(string message, ConsoleColor color)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = prev;

            // 즉시 출력(버퍼 비우기)
            Console.Out.Flush();
        }

        // 이름 추출
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
//일단은 몬스터도 스킬을 쓸 수 있게 설계

