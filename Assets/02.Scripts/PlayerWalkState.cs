// --- WALK STATE ---
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState(PlayerStateController player, StateMachine stateMachine) : base(player, stateMachine) { }

    private const string stateName = "Walk";

    public override void Enter()
    {
        base.Enter();
        playerController.player.MovingController.MaxStableMoveSpeed = 3f;
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

        if (playerController.player.PlayerInput.RunHeld && playerController.CanRun)
        {
            stateMachine.ChangeState(playerController.RunState);
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