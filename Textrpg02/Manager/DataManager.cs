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
            var equippedDataForSave = new DictionaryConvertData
            {
                keys = inventory.EquippedItems.Keys.ToList(),
                values = inventory.EquippedItems.Values.ToList()
            };

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

                    bonusMaxHp = character.BonusMaxHp,
                    bonusMaxMp = character.BonusMaxMp,
                    bonusAttack = character.BonusAttack,
                    bonusSkillAttack = character.BonusSkillAttack,
                    bonusArmor = character.BonusArmor,
                    bonusMagicResistance = character.BonusMagicResistance,
                },

                InventoryData = new InventoryData
                {
                    items = inventory.Items,
                    equipItemCount = inventory.EquipItemCount,
                    consumeItemCount = inventory.ConsumeItemCount,
                    equippedItems = equippedDataForSave,
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

        private Dictionary<EquipSlot, EquipItem> ReassembleEquippedItems(InventoryData inventoryData)
        {
            if (inventoryData.equippedItems == null || inventoryData.equippedItems.keys == null)
            {
                // 데이터가 저장되지 않았거나 손상된 경우, 빈 딕셔너리 반환
                return new Dictionary<EquipSlot, EquipItem>();
            }

            // 딕셔너리의 키(EquipSlot)와 값(EquipItem) 리스트를 Zip하여 다시 Dictionary로 만듭니다.
            return inventoryData.equippedItems.keys
                .Zip(inventoryData.equippedItems.values, (key, value) => new { Key = key, Value = value })
                .ToDictionary(x => x.Key, x => x.Value);
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

                Dictionary<EquipSlot, EquipItem> restoredEquipped = ReassembleEquippedItems(loadedData.InventoryData);

                character = Character.LoadData(loadedData.CharacterData);
                inventory = Inventory.LoadData(loadedData.InventoryData, restoredEquipped);

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
