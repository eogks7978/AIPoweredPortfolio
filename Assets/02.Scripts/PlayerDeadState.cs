using UnityEngine;

// --- DEAD STATE ---
public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(PlayerStateController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        playerController.player.Anim.Play("Death");
    }
}