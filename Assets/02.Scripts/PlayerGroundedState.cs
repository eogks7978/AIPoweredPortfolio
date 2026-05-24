using UnityEngine;

public abstract class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Update()
    {
        base.Update();

        if (!player.IsGrounded)
        {
            stateMachine.ChangeState(player.FallState);
            return;
        }

        // ⭕ 수정: 이제 일반 AttackState가 아니라, 분리된 'GroundAttackState'로 전환합니다.
        if (player.playerInputHandler.AttackPressed && stateMachine.CurrentState != player.GroundAttackState)
        {
            stateMachine.ChangeState(player.GroundAttackState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}