using Newtonsoft.Json;
using TextRPG.Data;
using TextRPG.Entity;

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
                    Name = character.Name,
                    MaxHp = character.MaxHp,
                    Hp = character.Hp,
                    MaxMp = character.MaxMp,
                    Mp = character.Mp,
                    Attack = character.Attack,
                    SkillAttack = character.SkillAttack,
                    Armor = character.Armor,
                    MagicResistance = character.MagicResistance,
                    Job = character.Job,

                    Level = character.Level,
                    Gold = character.Gold,
                    MaxExp = character.MaxExp,
                    Exp = character.Exp,
                    Stamina = character.Stamina,

                    BonusMaxHp = character.BonusMaxHp,
                    BonusMaxMp = character.BonusMaxMp,
                    BonusAttack = character.BonusAttack,
                    BonusSkillAttack = character.BonusSkillAttack,
                    BonusArmor = character.BonusArmor,
                    BonusMagicResistance = character.BonusMagicResistance,
                },

                InventoryData = new InventoryData
                {
                    Items = inventory.Items,
                    EquipItemCount = inventory.EquipItemCount,
                    ConsumeItemCount = inventory.ConsumeItemCount,
                    EquippedItems = inventory.EquippedItems,
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
                        TypeNameHandling = TypeNameHandling.Objects
                    });

                if (loadedData == null) return false;

                character = Character.LoadData(loadedData.CharacterData);
                inventory = Inventory.LoadData(loadedData.InventoryData);
                character.SetInventory(inventory);
                inventory.SetCharacter(character);

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
