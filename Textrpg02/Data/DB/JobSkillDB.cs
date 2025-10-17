using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Enum;
using System.Text.Json;

namespace TextRPG.Data.DB
{
    internal class JobSkillDB
    {
        public static Dictionary<JobType, Dictionary<int, List<int>>> JobSkillTable { get; private set; } = new();

        public JobSkillDB()
        {
            LoadJobSkillTable();
        }

        private void LoadJobSkillTable()
        {
            string path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Data\DB\job_skill_table.json");
            if (!File.Exists(path))
            {
                Console.WriteLine($"{path} 파일을 찾을 수 없습니다.");
                return;
            }

            string json = File.ReadAllText(path);
            var raw = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, List<int>>>>(json);

            JobSkillTable.Clear();

            if (raw == null)
            {
                Console.WriteLine("job_skill_table.json 파싱 실패 (데이터가 null입니다).");
                return;
            }

            foreach (var (jobStr, levelMap) in raw)
            {
                if (!System.Enum.TryParse(jobStr, out JobType job))
                    continue;
                //Enum.TryParse 은 기본 제공 기능인데, enum이 따로 정의되어있어서 system붙여야함

                var levelDict = new Dictionary<int, List<int>>();
                foreach (var (levelStr, skillIds) in levelMap)
                {
                    if (int.TryParse(levelStr, out int level))
                        levelDict[level] = skillIds ?? new List<int>();
                }

                JobSkillTable[job] = levelDict;
            }

            Console.WriteLine($"JobSkillTable 로딩 완료 ({JobSkillTable.Count}개의 직업)");
        }
    }

}
