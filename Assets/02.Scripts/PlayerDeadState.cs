using UnityEngine;

// --- DEAD STATE ---
public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        playerController.player.Anim.Play("Death");
        playerController.player.Rb.linearVelocity = Vector3.zero;
        playerController.player.Rb.isKinematic = true;
    }
}