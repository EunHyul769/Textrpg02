using Newtonsoft.Json;
using TextRPG.Item;

namespace TextRPG.Data.DB
{
    internal class ConsumeItemDB
    {
        private const string DATA_FILE_PATH = "ConsumeItems.json";

        public Dictionary<int, ConsumeItem> Items { get; private set; }

        public ConsumeItemDB()
        {
            LoadDataFromJson();
        }

        private void LoadDataFromJson()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string projectRootPath = Path.GetFullPath(Path.Combine(baseDirectory, "..", "..", "..", ".."));
            string dataFilePath = Path.Combine(projectRootPath, "Textrpg02", "Data", "Json", DATA_FILE_PATH);

            if (!File.Exists(dataFilePath))
            {
                // 파일이 없으면 오류 메시지를 출력하고 빈 DB로 초기화
                Console.WriteLine($"[DB Error] ConsumeItem 데이터 파일을 찾을 수 없습니다: {dataFilePath}");
                Items = new Dictionary<int, ConsumeItem>();
                return;
            }

            try
            {
                string jsonString = File.ReadAllText(dataFilePath);

                // List<ConsumeItem>으로 역직렬화 (JSON 파일 형식에 맞춤)
                List<ConsumeItem> loadedList = JsonConvert.DeserializeObject<List<ConsumeItem>>(jsonString);

                // List를 Dictionary로 변환 (ID를 Key로 사용)
                Items = loadedList.ToDictionary(item => item.ID, item => item);

                Console.WriteLine($"[DB Info] {Items.Count}개의 소비 아이템 데이터 로드 완료.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DB Error] ConsumeItem JSON 로드 중 오류 발생: {ex.Message}");
                Items = new Dictionary<int, ConsumeItem>();
            }
        }

        public ConsumeItem? GetByKey(int key)
        {
            if (Items.ContainsKey(key))
            {
                return Items[key].Clone() as ConsumeItem;
            }
            return null;
        }
    }
}
