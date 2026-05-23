// --- RUN STATE ---
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        string idleAnim = player.CurrentWeapon.RunAnimationName;

        player.Anim.Play(idleAnim);
    }

    public override void Update()
    {
        base.Update();

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(player.WalkState);
            return;
        }
        if (!player.IsMoving)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        player.Rb.linearVelocity = new Vector3(moveX * player.Stats.RunSpeed, player.Rb.linearVelocity.y, moveZ * player.Stats.RunSpeed);
    }
}