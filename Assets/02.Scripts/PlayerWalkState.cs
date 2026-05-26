// --- WALK STATE ---
using UnityEngine;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        playerController.player.CurrentMoveSpeed = 0f;
    }

    public override void Update()
    {
        base.Update();

        playerController.player.Anim.SetFloat("MoveSpeed", playerController.player.CurrentMoveSpeed);

        if (!playerController.IsMoving)
        {
            stateMachine.ChangeState(playerController.IdleState);
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

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        playerController.player.CurrentMoveSpeed =
            Mathf.Lerp(
                playerController.player.CurrentMoveSpeed,
                playerController.player.Stats.walkSpeed,
                Time.fixedDeltaTime * 10f);

        Vector3 moveDir = playerController.player.GetMoveDirection();

        Vector3 velocity = new Vector3(
            moveDir.x * playerController.player.CurrentMoveSpeed,
            playerController.player.Rb.linearVelocity.y,
            moveDir.z * playerController.player.CurrentMoveSpeed);

        playerController.player.Move(velocity);
    }
}