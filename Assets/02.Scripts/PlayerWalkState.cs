// --- WALK STATE ---
using UnityEngine;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        if (stateMachine.PreviousState == player.RunState)
            player.Anim.CrossFadeInFixedTime("Walk", 0.3f); // Run→Walk: 블렌딩 길게
        else
            player.Anim.CrossFadeInFixedTime("Walk", 0.02f); // Idle→Walk: 빠르게
    }

    public override void Update()
    {
        base.Update();

        player.Anim.speed = player.CurrentMoveSpeed / player.Stats.walkSpeed;
        player.Anim.SetFloat("MoveSpeed", player.CurrentMoveSpeed);

        if (!player.IsMoving)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }

        if (player.playerInputHandler.RunHeld && player.CanRun)
        {
            stateMachine.ChangeState(player.RunState);
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
        player.CurrentMoveSpeed = Mathf.Lerp(player.CurrentMoveSpeed, player.Stats.walkSpeed, Time.fixedDeltaTime * 10f);

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
            moveDir.x * player.Stats.walkSpeed,
            player.Rb.linearVelocity.y,
            moveDir.z * player.Stats.walkSpeed
        );

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