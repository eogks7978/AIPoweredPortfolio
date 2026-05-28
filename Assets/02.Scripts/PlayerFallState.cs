// --- FALL STATE (하강 상태) ---

using GLTFast.Schema;
using UnityEngine;

public class PlayerFallState : PlayerAirborneState
{
    public PlayerFallState(PlayerStateController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (playerController.player.MovingController.Motor.GroundingStatus.IsStableOnGround)
        {
            stateMachine.ChangeState(playerController.LandingState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        playerController.player.Anim.ResetTrigger("Fall");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        UpdatePhysicsInput();
    }
}