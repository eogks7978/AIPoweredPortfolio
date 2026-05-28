using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerStateController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        playerController.player.MovingController.MaxStableMoveSpeed = 0f;
        playerController.player.Anim.SetTrigger("Landing");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!playerController.player.MovingController.Motor.GroundingStatus.IsStableOnGround)
        {
            playerController.StateMachine.ChangeState(playerController.FallState);
        }

        UpdatePhysicsInput();
    }

    public override void Exit()
    {
        base.Exit();
    }

    // 애니메이션 이벤트로 호출
    public void NotifyLandingEnd()
    {
        stateMachine.ChangeState(playerController.IdleState);
    }
}
