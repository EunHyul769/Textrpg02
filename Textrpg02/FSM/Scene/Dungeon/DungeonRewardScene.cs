using TextRPG.Entity;

namespace TextRPG.FSM.Scene
{
    internal class DungeonRewardScene : SceneBase
    {
        static int dungeonLevel;

        private Character character;

        // 던전 정보
        private string dungeonName;
        private int requiredArmor;
        private int baseGoldReward;
        private int baseExpReward;
        private int baseHpLossMin;
        private int baseHpLossMax;

        // 보상 계산
        private int hpLoss;
        private int actualHpLoss;
        private int bonusPercent;
        private int finalGold;
        private int finalExp;


        public DungeonRewardScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "던전 결과";
            character = GameManager.Instance.Character;

            dungeonName = string.Empty;
            if (dungeonLevel == 1)
            {
                dungeonName = "쉬운 던전";
                requiredArmor = 5;
                baseGoldReward = 1000;
                baseExpReward = 50;
            }
            else if (dungeonLevel == 2)
            {
                dungeonName = "일반 던전";
                requiredArmor = 11;
                baseGoldReward = 1700;
                baseExpReward = 100;
            }
            else if (dungeonLevel == 3)
            {
                dungeonName = "어려운 던전";
                requiredArmor = 17;
                baseGoldReward = 2500;
                baseExpReward = 200;
            }
            baseHpLossMin = 20;
            baseHpLossMax = 35;
        }

        protected override void View()
        {
            Console.WriteLine("[던전 결과]\n");

            if (Calculate())
            {
                Console.WriteLine("축하합니다!");
                Console.WriteLine($"{dungeonName}을 클리어 하였습니다.\n");

                Console.WriteLine("탐험 결과");
                Console.WriteLine($"체력 {actualHpLoss} 감소. (남은 체력: {character.Hp})");
                Console.WriteLine($"골드 {finalGold} 획득. (추가 보상 {bonusPercent}%)");
                Console.WriteLine($"경험치 {finalExp} 획득. (추가 보상 {bonusPercent}%)\n");
            }
            else
            {
                Console.WriteLine("던전 실패...");
                Console.WriteLine($"체력 {hpLoss} 감소. (남은 체력: {character.Hp})");
                Console.WriteLine("보상 없음.\n");
            }

            ReturnToVillage();
        }

        protected override void Control()
        {
        }

        public static void LevelSetting(int level)
        {
            dungeonLevel = level;
        }

        private bool Calculate()
        {
            //던전 성공/ 실패 확률 판단(권장 방어력 기준)
            int defenseDiff = character.Armor - requiredArmor; // 내 방어력 - 권장 방어력
            int baseFailChance = 40; // 기본 실패 확률 (권장 방어력보다 낮을 때)

            // 권장 방어력보다 낮은 경우 (실패 확률 40%)
            if (character.Armor < requiredArmor)
            {
                int failChance = baseFailChance;
                if (Random.Next(1, 101) <= failChance)
                {
                    // 실패 처리
                    // 체력 감소: 절반
                    hpLoss = character.Hp / 2;
                    character.TakeHp(hpLoss);

                    return false;
                }
            }

            // 체력 소모량 계산 (기본 20~35 랜덤 + 방어력 차이 반영)
            int hpLossRangeMin = baseHpLossMin - defenseDiff;
            int hpLossRangeMax = baseHpLossMax - defenseDiff;

            // 체력 소모량 계산
            actualHpLoss = Random.Next(
                Math.Max(0, hpLossRangeMin),
                Math.Max(1, hpLossRangeMax + 1)
            );

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
