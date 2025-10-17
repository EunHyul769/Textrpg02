using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TextRPG.Data
{
    internal class SkillDB
    {
        public static Dictionary<int, Skill> Skills { get; private set; } = new();

        public SkillDB()
        {
            LoadSkillsFromJson();
        }

        private void LoadSkillsFromJson()
        {
            string path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Data\skills.json");

            if (!File.Exists(path))
            {
                Console.WriteLine("skills.json 파일을 찾을 수 없습니다.");
                return;
            }

            string json = File.ReadAllText(path);

            List<SkillDTO>? skillList = JsonSerializer.Deserialize<List<SkillDTO>>(json);
            if (skillList == null)
            {
                Console.WriteLine("skills.json 파일을 읽는 데 실패했습니다.");
                return;
            }

            foreach (var dto in skillList)
            {
                Skill skill = new Skill(dto.name, dto.power, dto.sPower, dto.flat, dto.mpCost, dto.isMultiHit, dto.hits);
                Skills[dto.id] = skill;
            }

            Console.WriteLine($"{Skills.Count}개의 스킬 데이터 로드 완료");
        }

        private class SkillDTO
        {
            public int id { get; set; }
            public string name { get; set; }
            public double power { get; set; }
            public double sPower { get; set; }
            public int flat { get; set; }
            public int mpCost { get; set; }
            public bool isMultiHit { get; set; }
            public int hits { get; set; }
        }
    }
}
