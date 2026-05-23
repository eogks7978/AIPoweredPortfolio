using UnityEngine;

// --- JUMP STATE (��� ����) ---
public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        player.Anim.Play("Jump");
    }

    public override void Update()
    {
        base.Update();

        if (player.Rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }
}