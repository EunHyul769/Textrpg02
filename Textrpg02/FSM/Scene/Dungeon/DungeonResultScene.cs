using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Data;
using TextRPG.Entity;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class DungeonResultScene : SceneBase
    {
        private Character player;

        // 던전 정보
        private int floorNum; //현재 층수
        private int baseGoldReward;
        private int baseExpReward;
        private int lastDungeon = 5; //마지막 던전 층수

        // 보상 계산
        private int hpLoss;
        private int actualHpLoss;
        private int bonusPercent;
        private int finalGold;
        private int finalExp;
        
        public DungeonResultScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "던전 결과";
            floorNum = GameManager.Instance.CurrentFloors;
            player = GameManager.Instance.Character;

            baseGoldReward = 1000 * floorNum;
            baseExpReward = 50 * floorNum;
        }

        
        protected override void View()
        {
        }

        protected override void Control()
        {
            if (DungeonManager.Instance.dungeonResult) //승리 시
            {
                Calculate();
                Console.WriteLine("축하합니다!");
                Console.WriteLine($"던전을 클리어 하였습니다.\n");

                Console.WriteLine("탐험 결과");
                Console.WriteLine($"남은 체력: {player.Hp}");
                Console.WriteLine($"골드 {finalGold} 획득. (추가 보상 {bonusPercent}%)");
                Console.WriteLine($"경험치 {finalExp} 획득. (추가 보상 {bonusPercent}%)\n");

                if (floorNum == lastDungeon) //마지막 던전이면 발동
                {
                    GameManager.Instance.CurrentFloors = 0; //층수 초기화
                    Console.Write("아무거나 입력하면 넘어갑니다.");
                    string input_ = Console.ReadLine();

                    ReturnToVillage();
                }

                GameManager.Instance.CurrentFloors++; // 층수 증가
                DungeonManager.Instance.dungeonResult = false; // 결과 초기화
                Console.Write("아무거나 입력하면 넘어갑니다.");
                string input = Console.ReadLine();
                controller.ChangeSceneState(controller.DungeonScene);
            }
            else
            {
                GameManager.Instance.CurrentFloors = 0; //층수 초기화

                hpLoss = player.Hp / 2;
                player.TakeHp(hpLoss);

                Console.WriteLine("던전 실패...");
                Console.WriteLine($"남은 체력: {player.Hp}");
                Console.WriteLine("보상 없음.\n");

                ReturnToVillage();
            }
        }

        private void Calculate()
        {
            // 보상 계산 (기본 보상 + 공격력 기반 추가 보상)
            // 공격력 ~ 공격력 * 2 % 추가 보상 계산
            int bonusPercentMin = player.Attack;
            int bonusPercentMax = player.Attack * 2;

            // 실제 추가 보상 % (예: 공격력 10이면 10~20% 사이)
            bonusPercent = Random.Next(bonusPercentMin, bonusPercentMax + 1);

            float bonusMultiplier = 1f + (bonusPercent / 100.0f);

            // 최종 보상 계산
            finalGold = (int)(baseGoldReward * bonusMultiplier);
            finalExp = (int)(baseExpReward * bonusMultiplier);

            // 능력치 반영
            player.AddGold(finalGold);
            player.TakeHp(actualHpLoss);
            player.AddExp(finalExp);
        }
    }
}
