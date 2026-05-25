// --- RUN STATE ---
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //playerController.player.Anim.speed = playerController.player.CurrentMoveSpeed / playerController.player.Stats.runSpeed;
        playerController.player.Anim.SetFloat("MoveSpeed", playerController.player.CurrentMoveSpeed);

        if (!playerController.IsMoving)
        {
            stateMachine.ChangeState(playerController.IdleState);
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

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        playerController.player.CurrentMoveSpeed =
            Mathf.Lerp(
                playerController.player.CurrentMoveSpeed,
                playerController.player.Stats.runSpeed,
                Time.fixedDeltaTime * 3f);

        Vector3 moveDir = playerController.player.GetMoveDirection();

        Vector3 velocity = new Vector3(
            moveDir.x * playerController.player.CurrentMoveSpeed,
            playerController.player.Rb.linearVelocity.y,
            moveDir.z * playerController.player.CurrentMoveSpeed);

        playerController.player.Move(velocity);
    }
}