// --- FALL STATE (하강 상태) ---

using UnityEngine;

public class PlayerFallState : PlayerAirborneState
{
    public PlayerFallState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update(); // 부모의 착지 검사가 작동하여 땅에 닿으면 자동으로 Idle로 탈출함

        if (player.IsGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}