// --- RUN STATE ---
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerStateController player, StateMachine stateMachine) : base(player, stateMachine) { }

    private const string stateName = "Run";

    public override void Enter()
    {
        base.Enter();
        playerController.player.MovingController.MaxStableMoveSpeed = 8f;
    }

    public override void Update()
    {
        base.Update();

        UpdatePhysicsInput();

        playerController.player.Anim.SetFloat("MoveSpeed", playerController.player.MovingController.MaxStableMoveSpeed);

        if (!playerController.IsMoving)
        {
            stateMachine.ChangeState(playerController.IdleState);
            return;
        }

        if (playerController.player.PlayerInput.isCrouching)
        {
            stateMachine.ChangeState(playerController.CrouchWalkState);
            return;
        }

        if (!playerController.player.PlayerInput.RunHeld)
        {
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