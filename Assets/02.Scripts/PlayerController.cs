using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    // FSM 핵심 인스턴스들
    public StateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerGroundedState GroundState { get; private set; }
    public PlayerGroundAttackState GroundAttackState { get; private set; }
    public PlayerLandingState LandingState { get; private set; }
    public PlayerAirborneState AirborneState { get; private set; }
    public PlayerAirAttackState AirAttackState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }

    [SerializeField] private AnimatorOverrideController swordOverride;
    [SerializeField] private AnimatorOverrideController gunOverride;

    [Header("Ground Check")]
    [SerializeField] private ColliderCrashChecker groundChecker;
    [SerializeField] private ColliderCrashChecker headBlockChecker;

    public PlayerCharacter player { get; private set; }

    public void OnJumpForce() => player.Rb.AddForce(Vector3.up * player.Stats.jumpForce, ForceMode.Impulse);
    public void OnLandingAnimEnd() => LandingState.NotifyLandingEnd();
    public void OnJumpAnimEnd() => JumpState.OnJumpAnimEnd();

    public bool IsMoving => player.PlayerInput.MoveInput != Vector2.zero;
    public bool CanJump => IsGrounded
        && StateMachine.CurrentState is PlayerGroundedState
        && StateMachine.CurrentState != GroundAttackState
        && !IsHeadBlocked;
    public bool CanRun => StateMachine.CurrentState == IdleState
        || StateMachine.CurrentState == WalkState;
    public bool CanChangeWeapon => StateMachine.CurrentState == IdleState;
    public bool IsGrounded => groundChecker.IsCrashed;
    public bool IsHeadBlocked => headBlockChecker.IsCrashed;
    public bool IsDead => StateMachine.CurrentState == DeadState;

    private void Awake()
    {
        StateMachine = new StateMachine();

        IdleState = new PlayerIdleState(this, StateMachine);
        WalkState = new PlayerWalkState(this, StateMachine);
        RunState = new PlayerRunState(this, StateMachine);
        GroundAttackState = new PlayerGroundAttackState(this, StateMachine);
        LandingState = new  PlayerLandingState(this, StateMachine);
        AirAttackState = new PlayerAirAttackState(this, StateMachine);
        JumpState = new PlayerJumpState(this, StateMachine);
        FallState = new PlayerFallState(this, StateMachine);
        DeadState = new PlayerDeadState(this, StateMachine);

        player = GetComponent<PlayerCharacter>();
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }
}