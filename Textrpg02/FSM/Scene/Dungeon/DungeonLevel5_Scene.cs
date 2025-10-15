using TextRPG.Entity;

namespace TextRPG.FSM.Scene
{
    internal class DungeonLevel5_Scene : SceneBase
    {
        private Character character;

        // 던전 정보
        private int baseGoldReward;
        private int baseExpReward;

        // 보상 계산
        private int hpLoss;
        private int actualHpLoss;
        private int bonusPercent;
        private int finalGold;
        private int finalExp;

        private int floorNum = 5; //현재 층수
        private string dungeonName = "하데스의 문"; //현재 던전 이름
        private string dungeonNameNext = ""; //다음 던전 이름
        public static bool battleResult; //전투 결과


        public DungeonLevel5_Scene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = $"{floorNum}층";
            character = GameManager.Instance.Character;

            baseGoldReward = (1000 * floorNum) * 2; //보상 골드 설정
            baseExpReward = (50 * floorNum) * 3; // 보상 경험치 설정
        }

        protected override void View()
        {
            Console.WriteLine($"[{floorNum}층: {dungeonName}]\n");
            Console.WriteLine("------[던전 결과]------\n");
        }

        protected override void Control()
        {
            if (battleResult) //승리 시
            {
                if (Calculate())
                {
                    Console.WriteLine("축하합니다!");
                    Console.WriteLine($"모든 던전을 클리어 하였습니다.\n");

                    Console.WriteLine("탐험 결과");
                    Console.WriteLine($"체력 {actualHpLoss} 감소. (남은 체력: {character.Hp})");
                    Console.WriteLine($"골드 {finalGold} 획득. (추가 보상 {bonusPercent}%)");
                    Console.WriteLine($"경험치 {finalExp} 획득. (추가 보상 {bonusPercent}%)\n");

                    ReturnToVillage();
                }
            }
            else
            {
                hpLoss = character.Hp / 2;
                character.TakeHp(hpLoss);

                Console.WriteLine("던전 실패...");
                Console.WriteLine($"체력 {hpLoss} 감소. (남은 체력: {character.Hp})");
                Console.WriteLine("보상 없음.\n");

                ReturnToVillage();
            }
        }
        
        public static bool BattleResult(bool result)
        {
            
            return result;
        }

        private bool Calculate()
        {
            // 보상 계산 (기본 보상 + 공격력 기반 추가 보상)
            // 공격력 ~ 공격력 * 2 % 추가 보상 계산
            int bonusPercentMin = character.Attack;
            int bonusPercentMax = character.Attack * 2;

            // 실제 추가 보상 % (예: 공격력 10이면 10~20% 사이)
            bonusPercent = Random.Next(bonusPercentMin, bonusPercentMax + 1);

            float bonusMultiplier = 1f + (bonusPercent / 100.0f);

            // 최종 보상 계산
            finalGold = (int)(baseGoldReward * bonusMultiplier);
            finalExp = (int)(baseExpReward * bonusMultiplier);

            // 능력치 반영
            character.AddGold(finalGold);
            character.TakeHp(actualHpLoss);
            character.AddExp(finalExp);

            return true;
        }
    }
}
