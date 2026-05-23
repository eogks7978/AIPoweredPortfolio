using UnityEngine;

public abstract class PlayerAirborneState : PlayerState
{
    public PlayerAirborneState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}