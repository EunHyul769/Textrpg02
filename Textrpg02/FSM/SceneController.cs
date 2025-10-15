using TextRPG.FSM.Scene;
using TextRPG.FSM.Scene.Dungeon;
using TextRPG.FSM.Scene.PlayerScene;
using TextRPG.FSM.Scene.Village;
using TextRPG.Interface;

namespace TextRPG.FSM
{
    // 씬 상태 전이를 관리하는 상태 기계
    internal class SceneController
    {
        public ISceneState CurrentState { get; private set; }

        // 던전 관련
        public ISceneState DungeonRewardScene { get; private set; }
        public ISceneState DungeonScene { get; private set; }
        public ISceneState DungeonResultScene { get; private set; }

        //배틀 관련 - 임시
        public ISceneState BattleLevel1_Scene { get; private set; }


        // 캐릭터/인벤토리 관련
        public ISceneState ConsumptionScene { get; private set; }
        public ISceneState CreateCharacterScene { get; private set; }
        public ISceneState EquipmentScene { get; private set; }
        public ISceneState InventoryScene { get; private set; }
        public ISceneState StatusScene { get; private set; }

        // 마을/활동 관련
        public ISceneState PatrolVillageScene { get; private set; }
        public ISceneState RandomAdventureScene { get; private set; }
        public ISceneState RestScene { get; private set; }
        public ISceneState ShopBuyScene { get; private set; }
        public ISceneState ShopScene { get; private set; }
        public ISceneState ShopSellScene { get; private set; }
        public ISceneState TrainingScene { get; private set; }
        public ISceneState VillageScene { get; private set; }

        public void Start()
        {
            // 던전 관련
            DungeonRewardScene = new DungeonRewardScene(this);
            DungeonScene = new DungeonScene(this);
            DungeonResultScene = new DungeonResultScene(this);

            //배틀 관련 - 임시
            BattleLevel1_Scene = new BattleLevel1_Scene(this);



            // 캐릭터/인벤토리 관련
            ConsumptionScene = new ConsumptionScene(this);
            CreateCharacterScene = new CreateCharacterScene(this);
            EquipmentScene = new EquipmentScene(this);
            InventoryScene = new InventoryScene(this);
            StatusScene = new StatusScene(this);

            // 마을/활동 관련
            PatrolVillageScene = new PatrolVillageScene(this);
            RandomAdventureScene = new RandomAdventureScene(this);
            RestScene = new RestScene(this);
            ShopBuyScene = new ShopBuyScene(this);
            ShopScene = new ShopScene(this);
            ShopSellScene = new ShopSellScene(this);
            TrainingScene = new TrainingScene(this);
            VillageScene = new VillageScene(this);

            ChangeSceneState(CreateCharacterScene);
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void ChangeSceneState(ISceneState newSceneState)
        {
            CurrentState?.Exit(); // 현재 상태 종료 (Exit)
            CurrentState = newSceneState;
            CurrentState.Enter(); // 새 상태 진입 (Enter)
        }
    }
}
