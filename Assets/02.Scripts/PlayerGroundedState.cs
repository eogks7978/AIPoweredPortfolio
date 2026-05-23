using UnityEngine;

public abstract class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Update()
    {
        base.Update();

        if (!player.IsMoving && stateMachine.CurrentState != player.IdleState)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }

        if (player.IsMoving)
        {
            stateMachine.ChangeState(player.WalkState);
            return;
        }

        // [공통] 지상에서는 언제든 점프 키를 누르면 점프 상태로 전환
        if (Input.GetKeyDown(KeyCode.Space) && player.CanJump)
        {
            stateMachine.ChangeState(player.JumpState);
            return;
        }

        // ⭕ 수정: 이제 일반 AttackState가 아니라, 분리된 'GroundAttackState'로 전환합니다.
        if (Input.GetKeyDown(KeyCode.Mouse0) && stateMachine.CurrentState != player.GroundAttackState)
        {
            stateMachine.ChangeState(player.GroundAttackState);
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift) && player.CanRun)
        {
            stateMachine.ChangeState(player.RunState);
            return;
        }

        // [공통] 만약 땅에서 벗어났다면 (예: 절벽에서 떨어짐) 곧바로 낙하 상태로 전환
        if (!player.IsGrounded)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // 💡 [참고] 혹시나 지상 공통 멈춤 판정이나 마찰력 제어 시 velocity를 쓰신다면
        // 아래처럼 linearVelocity를 사용하셔야 안전합니다.
        // player.Rb.linearVelocity = new Vector2(player.Rb.linearVelocity.x, player.Rb.linearVelocity.y);
    }
}