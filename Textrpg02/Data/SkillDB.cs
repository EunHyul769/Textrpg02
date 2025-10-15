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
            //id,    이름 물딜배율 마딜배율 소모mp
            {1, new Skill("더미1", 1.2, 0, 10) },

            //10번대 = Warrior
            {10, new Skill("더미2", 1.5, 0, 20) },

            //20번대 = Archer
            {20, new Skill("더미3", 0, 2.0, 15) },

            //30번대 = Mage
            {30, new Skill("더미4", 1.0, 1.0, 15) },
        };
    }
}
