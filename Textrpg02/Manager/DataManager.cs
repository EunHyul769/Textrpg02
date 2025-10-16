using Newtonsoft.Json;
using TextRPG.Data;
using TextRPG.Entity;
using TextRPG.Enum;
using TextRPG.Item;

namespace TextRPG.Manager
{
    internal class DataManager
    {
        private const string SAVE_FILE_NAME = "savegame.json";

        public GameData GameData { get; private set; }

        public DataManager()
        {
            GameData = new GameData();
        }

        private static DataManager instance;
        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataManager();
                }
                return instance;
            }
        }

        public void SaveGame(Character character, Inventory inventory)
        {
            var saveData = new GameSaveData
            {
                CharacterData = new CharacterData
                {
                    name = character.Name,
                    maxHp = character.MaxHp,
                    hp = character.Hp,
                    maxMp = character.MaxMp,
                    mp = character.Mp,
                    attack = character.Attack,
                    skillAttack = character.SkillAttack,
                    armor = character.Armor,
                    magicResistance = character.MagicResistance,
                    job = character.Job,

                    level = character.Level,
                    gold = character.Gold,
                    maxExp = character.MaxExp,
                    exp = character.Exp,
                    stamina = character.Stamina,
                },

                InventoryData = new InventoryData
                {
                    items = inventory.Items,
                    equipItemCount = inventory.EquipItemCount,
                    consumeItemCount = inventory.ConsumeItemCount,
                },
            };

            try
            {
                string jsonString = JsonConvert.SerializeObject(
                    saveData,
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        TypeNameHandling = TypeNameHandling.Objects
                    });
                File.WriteAllText(SAVE_FILE_NAME, jsonString);
                Console.WriteLine("\n[시스템] 게임 상태가 성공적으로 저장되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[저장 오류] 파일 쓰기 실패: {ex.Message}");
            }
        }

        public bool LoadGame(out Character character, out Inventory inventory)
        {
            character = null;
            inventory = null;

            if (!File.Exists(SAVE_FILE_NAME))
            {
                Console.WriteLine("[불러오기] 저장된 파일이 없습니다.");
                return false;
            }

            try
            {
                string jsonString = File.ReadAllText(SAVE_FILE_NAME);
                var loadedData = JsonConvert.DeserializeObject<GameSaveData>(
                    jsonString,
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        TypeNameHandling = TypeNameHandling.Objects
                    });

                if (loadedData == null) return false;

                character = Character.LoadData(loadedData.CharacterData);
                inventory = Inventory.LoadData(loadedData.InventoryData, character);

                character.SetInventory(inventory);

                Console.WriteLine("[불러오기 완료] 게임 상태를 복원했습니다.");
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[불러오기 오류] 복원 실패: {ex.Message}");
                return false;
            }
        }
    }
}
