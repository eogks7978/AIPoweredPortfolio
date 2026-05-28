using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        playerController.player.Anim.SetFloat("MoveSpeed", 0f);
        playerController.player.MovingController.MaxStableMoveSpeed = 0f;
        playerController.player.Anim.SetTrigger("Idle");
    }

    public override void Update()
    {
        base.Update();

        UpdatePhysicsInput();

        if (playerController.IsMoving)
        {
            if (playerController.player.PlayerInput.RunHeld && playerController.CanRun)
            {
                stateMachine.ChangeState(playerController.RunState);
                return;
            }

            stateMachine.ChangeState(playerController.WalkState);
            return;
        }

        if (playerController.player.PlayerInput.JumpPressed && playerController.CanJump)
        {
            stateMachine.ChangeState(playerController.JumpState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}