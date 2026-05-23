public class StateMachine
{
    public State CurrentState { get; private set; }

    // 최초 시작 상태 설정
    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // 상태 교체 (기존 상태 Exit -> 새 상태 등록 -> 새 상태 Enter)
    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}