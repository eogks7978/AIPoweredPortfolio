using Unity.VisualScripting;
using UnityEngine;

// --- JUMP STATE (��� ����) ---
public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState(PlayerStateController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        playerController.player.Anim.SetTrigger("Jump");
    }

    public override void Update()
    {
        base.Update();

        if (playerController.player.MovingController.Motor.BaseVelocity.y < 0f
            || playerController.HeadCrashed)
        {
            stateMachine.ChangeState(playerController.FallState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        UpdatePhysicsInput();
    }

    public override void Exit()
    {
        base.Exit();
        playerController.player.Anim.ResetTrigger("Jump");
    }

    public void OnJumpAnimEnd()
    {
        playerController.player.Anim.SetTrigger("Fall");
    }
}