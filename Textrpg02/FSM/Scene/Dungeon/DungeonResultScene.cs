using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Entity;

namespace TextRPG.FSM.Scene.Dungeon
{
    internal class DungeonResultScene : SceneBase
    {
        static int dungeonLevel;

        private Character character;

        // 던전 정보
        private string dungeonName;
        private int floorNum; //현재 층수
        private int baseGoldReward;
        private int baseExpReward;
        private bool lastDungeon;

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
            character = GameManager.Instance.Character;

            switch (DungeonManager.Instance.dungeonStage)
            {
                case 0:
                    floorNum = 1;
                    dungeonName = "네메아 사자의 방";
                    baseGoldReward = 1000;
                    baseExpReward = 50;
                    break;
                case 1:
                    floorNum = 2;
                    dungeonName = "레나르의 호수";
                    baseGoldReward = 1700;
                    baseExpReward = 100;
                    break;
                case 2:
                    floorNum = 3;
                    dungeonName = "케리네이아의 야영지";
                    baseGoldReward = 2500;
                    baseExpReward = 200;
                    break;
                case 3:
                    floorNum = 4;
                    dungeonName = "에리만토스의 산림";
                    baseGoldReward = 3500;
                    baseExpReward = 300;
                    break;
                case 4:
                    lastDungeon = true; //마지막 던전 확인
                    floorNum = 5;
                    dungeonName = "하데스의 문";
                    baseGoldReward = 5000;
                    baseExpReward = 500;
                    break;
                default:
                    break;
            }
        }

        
        protected override void View()
        {
        }

        protected override void Control()
        {
            if (DungeonManager.Instance.dungeonResult) //승리 시
            {
                if (Calculate())
                {
                    Console.WriteLine("축하합니다!");
                    Console.WriteLine($"{dungeonName}을/를 클리어 하였습니다.\n");

                    Console.WriteLine("탐험 결과");
                    Console.WriteLine($"체력 {actualHpLoss} 감소. (남은 체력: {character.Hp})");
                    Console.WriteLine($"골드 {finalGold} 획득. (추가 보상 {bonusPercent}%)");
                    Console.WriteLine($"경험치 {finalExp} 획득. (추가 보상 {bonusPercent}%)\n");

                    if (lastDungeon) //마지막 던전이면 발동
                    {
                        DungeonManager.Instance.dungeonStage = 0; //층수 초기화
                        lastDungeon = false; //값 초기화
                        Console.Write("아무 거나 입력하면 넘어갑니다.");
                        string input_ = Console.ReadLine();

                        ReturnToVillage();
                    }

                    DungeonManager.Instance.dungeonStage++; // 층수 증가
                    DungeonManager.Instance.dungeonResult = false; // 결과 초기화
                    Console.Write("아무 거나 입력하면 넘어갑니다.");
                    string input = Console.ReadLine();
                    controller.ChangeSceneState(controller.DungeonScene);
                }
            }
            else
            {
                DungeonManager.Instance.dungeonStage = 0; //층수 초기화

                hpLoss = character.Hp / 2;
                character.TakeHp(hpLoss);

                Console.WriteLine("던전 실패...");
                Console.WriteLine($"체력 {hpLoss} 감소. (남은 체력: {character.Hp})");
                Console.WriteLine("보상 없음.\n");

                ReturnToVillage();
            }
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
