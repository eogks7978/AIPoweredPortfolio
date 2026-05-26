using UnityEngine;

// --- JUMP STATE (��� ����) ---
public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        playerController.player.Anim.SetTrigger("Jump");
    }

    public override void Update()
    {
        base.Update();

        if (playerController.player.Rb.linearVelocity.y < -0.1f)
        {
            stateMachine.ChangeState(playerController.FallState);
        }
    }

    public void OnJumpAnimEnd()
    {
        playerController.player.Anim.SetTrigger("Fall");
    }
}