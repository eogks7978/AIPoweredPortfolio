// --- WALK STATE ---
using UnityEngine;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        string idleAnim = player.CurrentWeapon.WalkAnimationName;

        player.Anim.Play(idleAnim);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        player.Rb.linearVelocity = new Vector3(moveX * player.Stats.WalkSpeed, player.Rb.linearVelocity.y, moveZ * player.Stats.WalkSpeed);
    }
}