public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        // 💡 플레이어가 어떤 무기를 들고 있든, 그 무기의 Idle 애니메이션 이름을 가져옵니다.
        string idleAnim = player.CurrentWeapon.IdleAnimationName;

        // 가져온 이름으로 애니메이션을 재생합니다. (예: "Idle_Sword", "Idle_Gun")
        player.Anim.Play(idleAnim);
    }

    public override void Update()
    {
        base.Update();

        // 이동 입력이 들어오면 MoveState로 전환하는 등의 기존 로직...
        //if (player.InputX != 0)
        //{
        //    stateMachine.ChangeState(player.MoveState);
        //}
    }
}