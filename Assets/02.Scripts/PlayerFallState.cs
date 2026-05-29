// --- FALL STATE (하강 상태) ---

using GLTFast.Schema;
using UnityEngine;

public class PlayerFallState : PlayerAirborneState
{
    public PlayerFallState(PlayerStateController player, StateMachine stateMachine) : base(player, stateMachine) { }

    private const string stateName = "Falling Idle";

    public override void Enter()
    {
        base.Enter();
        playerController.player.Anim.CrossFadeInFixedTime(stateName, 0.3f);
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
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        UpdatePhysicsInput();
    }
}