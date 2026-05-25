using UnityEngine;

public abstract class PlayerState : State
{
    protected PlayerController playerController; // 플레이어 전용 리모컨 추가

    // 부모 State의 생성자에게 stateMachine을 패스해줍니다.
    public PlayerState(PlayerController player, StateMachine stateMachine) : base(stateMachine)
    {
        this.playerController = player;
    }
}