using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }

    // 최초 시작 상태 설정
    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // 상태 교체 (기존 상태 Exit -> 새 상태 등록 -> 새 상태 Enter)
    public void ChangeState(State newState)
    {
        if (newState == CurrentState) return;

        if (CurrentState != null)
            CurrentState.Exit();

        PreviousState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter();
        Debug.Log("최신상태 :" + newState);
    }
}