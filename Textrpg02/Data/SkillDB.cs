using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Entity;

namespace TextRPG.Data
{
    internal class SkillDB
    {
        //스킬 딕셔너리
        public static Dictionary<int, Skill> Skills = new Dictionary<int, Skill>()
        {   // 직업별 10번대로 분리해야 관리가 편함
            //id, 이름 물딜배율 마딜배율 고정치 소모mp 다단히트? 타격횟수

            {1, new Skill("더미1", 1.0, 0, 10, 1) }, //실제로는 리스트에 안 넣을 완전 더미 스킬

            //10번대 = Warrior. 배율은 낮되 고정뎀을 이용해서 안정적으로 딜을 가져가는 컨셉
            {10, new Skill("강타", 1.5, 0, 5, 3) },
            {11, new Skill("이연참", 1.0, 0, 5, 5, true, 2) },
            {12, new Skill("펀토", 2.5, 0, 8, 8) },
            {13, new Skill("승룡검", 3.0, 0, 10, 10) },

            //20번대 = Archer   정석적인 스킬 ad딜러
            {20, new Skill("더블 샷", 0.75, 0, 0, 3, true, 2) },
            {21, new Skill("속사", 2.7, 0, 0, 5) },
            {22, new Skill("일괄 사격", 3.5, 0, 0, 8) },

            //30번대 = Mage   화력. 그리고 또 화력.
            {30, new Skill("매직 미사일", 0, 3.0, 0, 5) },
            {31, new Skill("파이어 볼", 0, 4.0, 0, 8) },
            {32, new Skill("아이스 스피어", 0, 5.0, 0, 11) },
            {33, new Skill("스톤 샤워", 0, 2.0, 0, 15, true, 3) },
            {34, new Skill("윈드 엣지", 0, 3.5, 10, 20, true, 2) },
            {35, new Skill("콜 라이트닝", 0, 8.5, 0, 25) },
            {36, new Skill("익스팅션 레이", 0, 10.0, 10, 32) },
            {37, new Skill("익스플로젼", 0, 12.0, 0, 35) },
            {38, new Skill("템페스트", 0, 2.9, 0, 42, true, 5) },
            {39, new Skill("슈퍼 노바", 0, 16.0, 20, 50) },
        };
    }
}
