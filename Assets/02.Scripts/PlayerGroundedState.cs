using UnityEngine;

public abstract class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Update()
    {
        base.Update();

        // ⭕ 수정: 이제 일반 AttackState가 아니라, 분리된 'GroundAttackState'로 전환합니다.
        if (playerController.player.PlayerInput.AttackPressed && stateMachine.CurrentState != playerController.GroundAttackState)
        {
            stateMachine.ChangeState(playerController.GroundAttackState);
            return;
        }

        if (playerController.player.MovingController.Motor.BaseVelocity.y < -8f)
        {
            stateMachine.ChangeState(playerController.FallState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}