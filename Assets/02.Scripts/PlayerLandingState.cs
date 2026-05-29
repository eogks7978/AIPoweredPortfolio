using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerStateController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    private const string stateName = "Landing";

    public override void Enter()
    {
        base.Enter();
        playerController.player.MovingController.MaxStableMoveSpeed = 0f;
        playerController.player.Anim.CrossFadeInFixedTime(stateName, 0.1f);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (playerController.player.MovingController.Motor.BaseVelocity.y < -8f)
        {
            stateMachine.ChangeState(playerController.FallState);
            return;
        }

        UpdatePhysicsInput();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void UpdatePhysicsInput()
    {
        PlayerInputs input = playerController.player.PlayerInput.HandleCharacterInput();

        input.JumpDown = false;

        playerController.player.MovingController.SetInputs(ref input);
    }


    // 애니메이션 이벤트로 호출
    public void NotifyLandingEnd()
    {
        stateMachine.ChangeState(playerController.IdleState);
    }
}
