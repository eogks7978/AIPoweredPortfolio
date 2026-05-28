using UnityEngine;

public abstract class PlayerState : State
{
    protected PlayerStateController playerController; // 플레이어 전용 리모컨 추가

    // 부모 State의 생성자에게 stateMachine을 패스해줍니다.
    public PlayerState(PlayerStateController player, StateMachine stateMachine) : base(stateMachine)
    {
        this.playerController = player;
    }

    public virtual void UpdatePhysicsInput()
    {
        PlayerInputs input = playerController.player.PlayerInput.HandleCharacterInput();

        playerController.player.MovingController.SetInputs(ref input);
    }
}