
namespace TextRPG.Interface
{
    internal interface ISceneState
    {
        void Enter();   // 씬 진입 시 실행
        void Update();  // 로직 처리
        void Exit();    // 씬 떠날 때 실행
    }
}
