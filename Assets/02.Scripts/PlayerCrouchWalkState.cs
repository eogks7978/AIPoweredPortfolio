using UnityEngine;

public class PlayerCrouchWalkState : PlayerGroundedState
{
    public PlayerCrouchWalkState(PlayerStateController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    private const string stateName = "CrouchWalk";

    public override void Enter()
    {
        base.Enter();
        playerController.player.MovingController.MaxStableMoveSpeed = 2f;
        playerController.player.Anim.CrossFadeInFixedTime(stateName, 0.1f);
    }

    public override void Update()
    {
        base.Update();

        UpdatePhysicsInput();

        if (!playerController.IsMoving)
        {
            stateMachine.ChangeState(playerController.CrouchState);
            return;
        }

        if (!playerController.player.PlayerInput.isCrouching && !playerController.HeadCrashed)
        {
            stateMachine.ChangeState(playerController.IdleState);
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
