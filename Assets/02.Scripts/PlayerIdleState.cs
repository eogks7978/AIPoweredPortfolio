using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.CurrentMoveSpeed = 0f;
        player.Rb.linearVelocity = new Vector3(0f, 0f, 0f);
        player.Anim.CrossFadeInFixedTime("Idle", 0.2f);
    }

    public override void Update()
    {
        base.Update();

        if (player.IsMoving)
        {
            if (player.playerInputHandler.RunHeld && player.CanRun)
            {
                stateMachine.ChangeState(player.RunState);
                return;
            }

            stateMachine.ChangeState(player.WalkState);
            return;
        }

        if (player.playerInputHandler.JumpPressed && player.CanJump)
        {
            stateMachine.ChangeState(player.JumpState);
            return;
        }
    }
}