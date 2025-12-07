using TextRPG.Interface;

namespace TextRPG.FSM.Scene
{
    internal abstract class SceneBase : ISceneState
    {
        protected SceneController controller;

        protected Random Random { get; } = new Random();

        public SceneBase(SceneController controller)
        {
            this.controller = controller;
        }

        public virtual void Enter()
        {
            // [템플릿] 씬 진입 시 화면 정리 및 메뉴 루프 시작
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            SetScene();
        }

        public virtual void Update()
        {
            // 주 메뉴 루프 (템플릿)
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            View(); // 자식 클래스에서 메뉴 내용 정의
            Control(); // [템플릿] 입력 처리 및 상태 업데이트 로직 호출
        }

        public virtual void Exit()
        {
            // 씬을 떠날 때 필요한 정리 작업
        }

        // 템플릿 메서드 (자식 클래스에서 반드시 구현)

        // 씬 제목 설정 및 캐싱
        protected abstract void SetScene(); // Console.Title = "상점"

        // 씬의 메뉴 옵션을 출력합니다.
        protected abstract void View();

        // 사용자 입력을 받고 처리하거나, 따로 씬에서 수행하는 기능 구현
        protected abstract void Control();

        public void Sleep()
        {
            Thread.Sleep(500); // 대기
        }

        public void ReturnToVillage()
        {
            Console.WriteLine("\n아무 키나 눌러 마을로 돌아가세요...");
            Console.ReadKey(true);
            controller.ChangeSceneState(controller.VillageScene);
        }
    }
}
