using GLTFast.Schema;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    private const string stateName = "Idle";

    public override void Enter()
    {
        base.Enter();
        playerController.player.Anim.SetFloat("MoveSpeed", 0f);
        playerController.player.Anim.CrossFadeInFixedTime(stateName, 0.15f);
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

        if (playerController.player.PlayerInput.isCrouching)
        {
            stateMachine.ChangeState(playerController.CrouchState);
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