using UnityEngine;

public abstract class PlayerAirborneState : PlayerState
{
    public PlayerAirborneState(PlayerStateController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}