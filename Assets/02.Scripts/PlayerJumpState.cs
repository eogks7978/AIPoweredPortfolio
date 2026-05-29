using Unity.VisualScripting;
using UnityEngine;

// --- JUMP STATE (��� ����) ---
public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState(PlayerStateController player, StateMachine stateMachine) : base(player, stateMachine) { }

    private const string stateName = "Jump";

    public override void Enter()
    {
        base.Enter();

        playerController.player.Anim.CrossFadeInFixedTime(stateName, 0.1f);
    }

    public override void Update()
    {
        base.Update();

        if (playerController.HeadCrashed)
        {
            stateMachine.ChangeState(playerController.FallState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        UpdatePhysicsInput();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void NotifyJumpingEnd()
    {
        stateMachine.ChangeState(playerController.FallState);
    }
}