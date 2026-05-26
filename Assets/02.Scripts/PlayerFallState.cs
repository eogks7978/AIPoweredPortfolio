// --- FALL STATE (하강 상태) ---

using GLTFast.Schema;
using UnityEngine;

public class PlayerFallState : PlayerAirborneState
{
    public PlayerFallState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        AnimatorStateInfo stateInfo =
            playerController.player.Anim.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Falling Idle"))
            playerController.player.Anim.SetTrigger("Fall");
    }

    public override void Update()
    {
        base.Update();

        if (playerController.IsGrounded)
        {
            stateMachine.ChangeState(playerController.LandingState);
        }
    }
}