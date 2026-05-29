using UnityEngine;

public class PlayerCrouchState : PlayerGroundedState
{
    public PlayerCrouchState(PlayerStateController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    private const string stateName = "Crouch";

    public override void Enter()
    {
        base.Enter();
        playerController.player.MovingController.MaxStableMoveSpeed = 0f;
        playerController.player.Anim.CrossFadeInFixedTime(stateName, 0.1f);
    }

    public override void Update()
    {
        base.Update();

        UpdatePhysicsInput();

        if (playerController.IsMoving)
        {
            stateMachine.ChangeState(playerController.CrouchWalkState);
            return;
        }

        if (playerController.player.PlayerInput.JumpPressed && playerController.CanJump)
        {
            stateMachine.ChangeState(playerController.JumpState);
            return;
        }

        if (!playerController.player.PlayerInput.isCrouching && !playerController.HeadCrashed)
        {
            stateMachine.ChangeState(playerController.IdleState);
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
