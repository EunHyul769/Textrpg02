
namespace TextRPG.Entity
{
    internal class Skill
    {
        public string Name { get; set; }
        public double Power { get; set; }  // 물딜 배율
        public double SPower { get; set; }  //마딜 배율

        public int StaticValue { get; set; } //계수 없는 고정 수치

        public int MpCost { get; set; }

        public Skill(string name, double power, double spower, int sv, int mpCost)
        {
            Name = name;
            Power = power;
            SPower = spower;
            StaticValue = sv;
            MpCost = mpCost;
        }

        //물리 배율과 마법 배율이 존재하지 않으면 0으로 취급. double이라 값없으면 자동 0으로 적용
        public double CalculateSkillDamage(Character character)
        {
            double addamage = character.Attack * Power;    //물딜 따로 계산.
            double apdamage = character.SkillAttack * SPower;  //마딜 따로 계산
            int staticdamage = StaticValue;                 //고정 데미지는 스킬 표기값 그대로 적용

            double totaldamage = addamage + apdamage + staticdamage;   //총 딜로 합산

            return Math.Round(totaldamage, 0);  //소수 전부 반올림
        }
        // 나중엔 방어력 계수나 체력 계수, 스피드 계수 등도 추가가능


        

    }


}


    
