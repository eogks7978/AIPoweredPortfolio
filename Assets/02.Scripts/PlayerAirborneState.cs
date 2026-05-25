using UnityEngine;

public abstract class PlayerAirborneState : PlayerState
{
    private Vector3 savedVelocity;

    public PlayerAirborneState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        savedVelocity = new Vector3(
            playerController.player.Rb.linearVelocity.x,
            0f,
            playerController.player.Rb.linearVelocity.z
);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        AirMove();
    }

    public void AirMove()
    {
        Vector3 moveDir = playerController.player.GetMoveDirection();
        float airMoveSpeed = playerController.player.Stats.walkSpeed * 0.2f;

        // 이동 입력 속도 + 점프 시작 속도를 합산
        Vector3 airVelocity = (moveDir * airMoveSpeed) + savedVelocity;

        Vector3 horizontal = new Vector3(airVelocity.x, 0f, airVelocity.z);

        if (horizontal.magnitude > playerController.player.maxMoveSpeed)
        {
            horizontal = horizontal.normalized * playerController.player.maxMoveSpeed;
        }

        playerController.player.Rb.linearVelocity = new Vector3(horizontal.x, playerController.player.Rb.linearVelocity.y, horizontal.z);
    }
}