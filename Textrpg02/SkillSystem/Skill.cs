using TextRPG.Entity;

namespace TextRPG.SkillSystem
{
    internal class Skill
    {
        public string Name { get; set; }
        public double Power { get; set; }  // 물딜 배율
        public double SPower { get; set; }  //마딜 배율

        public int StaticValue { get; set; } //계수 없는 고정 수치

        public int MpCost { get; set; }

        public bool IsMultiHit { get; set; }    //다단히트인가 아닌가 판정
        public int MultiHitCount { get; set; }  //몇 번의 다단히트인가

        public Skill(string name, double power, double spower, int sv, int mpCost, bool isMultiHit=false, int multiHitCount=1)
        {
            Name = name;
            Power = power;
            SPower = spower;
            StaticValue = sv;
            MpCost = mpCost;
            IsMultiHit = isMultiHit;
            MultiHitCount = multiHitCount;
        }

        //물리 배율과 마법 배율이 존재하지 않으면 0으로 취급. double이라 값없으면 자동 0으로 적용
        //다단히트 스킬이 각 타격마다 기본 데미지 순차적 반환
        //각각 타격의 데미지만 결정해서 계산기쪽으로 넘김. 크리티컬과 방어력 등 최종 계산은 그쪽에서.
        public IEnumerable<double> CalculateSkillDamage(Character character)
        {
            int hits = IsMultiHit ? MultiHitCount : 1;

            for(int i = 0; i < hits; i++)
            {
                int adatk = character.GetRandomizedAttack();
                int apatk = character.GetRandomizedSkillAttack();

                double addamage = adatk * Power;   //물딜 따로 계산.
                double apdamage = apatk * SPower;  //마딜 따로 계산
                int staticdamage = StaticValue;    //고정 데미지는 스킬 표기값 그대로 적용

                double totaldamage = addamage + apdamage + staticdamage;   //총 딜로 합산

                yield return Math.Round(totaldamage, 0);  //소수 전부 반올림
            }
            
        }
        // 나중엔 방어력 계수나 체력 계수, 스피드 계수 등도 추가가능


        

    }


}


    
