using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        playerController.player.Rb.linearVelocity = new Vector3(0f, playerController.player.Rb.linearVelocity.y, 0f);
        playerController.player.Anim.SetTrigger("Landing");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!playerController.IsGrounded)
        {
            playerController.StateMachine.ChangeState(playerController.FallState);
        }
    }

    // 애니메이션 이벤트로 호출
    public void NotifyLandingEnd()
    {
        stateMachine.ChangeState(playerController.IdleState);
    }
}
