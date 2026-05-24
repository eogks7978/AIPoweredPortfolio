// --- RUN STATE ---
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        if (stateMachine.PreviousState == player.WalkState)
            player.Anim.CrossFadeInFixedTime("Run", 0.2f); // Walk→Run: 블렌딩 길게
        else
            player.Anim.CrossFadeInFixedTime("Run", 0.05f); // Idle→Run: 빠르게
    }

    public override void Update()
    {
        base.Update();

        player.Anim.speed = player.CurrentMoveSpeed / player.Stats.runSpeed;
        player.Anim.SetFloat("MoveSpeed", player.CurrentMoveSpeed);

        if (!player.IsMoving)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }

        if (!player.playerInputHandler.RunHeld)
        {
            stateMachine.ChangeState(player.WalkState);
            return;
        }

        if (player.playerInputHandler.JumpPressed && player.CanJump)
        {
            stateMachine.ChangeState(player.JumpState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.CurrentMoveSpeed = Mathf.Lerp(player.CurrentMoveSpeed, player.Stats.runSpeed, Time.fixedDeltaTime * 3f);

        float moveX = player.playerInputHandler.MoveInput.x;
        float moveZ = player.playerInputHandler.MoveInput.y;
        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 moveDir = (forward * moveZ + right * moveX).normalized;

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            player.transform.rotation = Quaternion.Slerp(
                player.transform.rotation,
                targetRotation,
                Time.fixedDeltaTime * 1f
            );
        }

        player.Rb.linearVelocity = new Vector3(
            moveDir.x * player.CurrentMoveSpeed,
            player.Rb.linearVelocity.y,
            moveDir.z * player.CurrentMoveSpeed);

        Vector3 roatationDir = player.GetMoveDirection();

        if (roatationDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(roatationDir);
            player.transform.rotation = Quaternion.Slerp(
                player.transform.rotation,
                targetRotation,
                Time.fixedDeltaTime * 10f
            );
        }
    }
}