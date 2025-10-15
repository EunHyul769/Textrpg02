
using TextRPG.Entity; // Player, Monster 클래스를 사용하기 위함

namespace TextRPG.Calculator
{
    internal class DamageCalculator
    {
        private static Random rng = new Random();

        // 공통 계산: 공격자(플레이어나 몬스터), 수비자, 스킬(있을 수도 있음)
        public static int CalculateAttack(object attacker, object defender, Skill? skill = null)
        {
            int attackerAtk;
            int defenderDef;
            double critChance = 0.0;
            double critMultiplier = 1.0;
            bool fromSkill = skill != null;
            double power = 1.0;
            bool ignoreDef = false;

            // 공격자 타입 판별
            if (attacker is Character character)
            {
                attackerAtk = character.Attack;
                critChance = character.CritChance;
                critMultiplier = character.CritMultiplier;
                if (skill != null)
                {
                    power = skill.PowerMultiplier;
                    ignoreDef = skill.IgnoresDefense;
                    critChance = 0.0; // 스킬에는 크리티컬 적용 안 함
                }
            }
            else if (attacker is Monster monster)
            {
                attackerAtk = monster.Atk;
                if (skill != null)
                {
                    power = skill.PowerMultiplier;
                    ignoreDef = skill.IgnoresDefense;
                }
            }
            else
            {
                throw new ArgumentException("공격자는 Character 또는 Monster여야 합니다.");
            }

            // 수비자 방어력
            if (defender is Character defChar)
                defenderDef = defChar.Armor;
            else if (defender is Monster defMon)
                defenderDef = defMon.Def;
            else
                throw new ArgumentException("수비자는 Character 또는 Monster여야 합니다.");

            return CalculateDamage(attackerAtk, defenderDef, critChance, critMultiplier, power, ignoreDef, fromSkill);
        }

        private static int CalculateDamage(
            int attackerAtk,
            int defenderDef,
            double critChance,
            double critMultiplier,
            double powerMultiplier,
            bool ignoreDef,
            bool fromSkill)
        {
            int defense = ignoreDef ? 0 : defenderDef / 2;
            int baseDamage = Math.Max(1, attackerAtk - defense);
            baseDamage = (int)(baseDamage * powerMultiplier);

            bool isCrit = !fromSkill && rng.NextDouble() < critChance;
            if (isCrit)
            {
                baseDamage = (int)Math.Round(baseDamage * critMultiplier);
                Console.WriteLine("★ 크리티컬 히트! ★");
            }

            return baseDamage;
        }
    }
}
//캐릭터cs에 CritChance, CritMultiplier 속성 추가
//스킬cs에 PowerMultiplier, IgnoresDefense 속성 추가
//ignoresDefense는 방어력 무시 여부

