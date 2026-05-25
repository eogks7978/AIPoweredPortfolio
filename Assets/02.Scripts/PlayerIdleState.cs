using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        playerController.player.Anim.SetFloat("MoveSpeed", 0f);
        playerController.player.Anim.SetTrigger("Idle");
        playerController.player.Rb.linearVelocity = new Vector3(0f, 0f, 0f);
    }

    public override void Update()
    {
        base.Update();

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
}