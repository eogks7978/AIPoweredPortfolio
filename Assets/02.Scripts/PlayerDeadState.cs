using UnityEngine;

// --- DEAD STATE ---
public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.Anim.Play("Death");
        player.Rb.linearVelocity = Vector3.zero;
        player.Rb.isKinematic = true;
    }
}