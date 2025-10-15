
namespace TextRPG.Data
{
    internal class GameSaveData
    {
        public CharacterData CharacterData { get; set; } = new CharacterData();

        public InventoryData InventoryData { get; set; } = new InventoryData();
    }
}
