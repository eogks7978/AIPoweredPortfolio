using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.Anim.CrossFadeInFixedTime("Landing", 0.2f);
    }

    public override void Update()
    {
        base.Update();
    }

    // 애니메이션 이벤트로 호출
    public void NotifyLandingEnd()
    {
        stateMachine.ChangeState(player.IdleState);
    }
}
