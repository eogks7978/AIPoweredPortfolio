using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerStateController : MonoBehaviour
{
    // FSM 핵심 인스턴스들
    public StateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerCrouchState CrouchState { get; private set; }
    public PlayerCrouchWalkState CrouchWalkState { get; private set; }
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
    [SerializeField] private ColliderCrashChecker headCrashCheck;

    public PlayerCharacter player { get; private set; }

    public bool IsMoving => player.PlayerInput.MoveInput != Vector2.zero;
    public bool CanJump => player.MovingController.Motor.GroundingStatus.IsStableOnGround
        && StateMachine.CurrentState != GroundAttackState;
    public bool CanRun => player.MovingController.Motor.GroundingStatus.IsStableOnGround 
        && (StateMachine.CurrentState == IdleState || StateMachine.CurrentState == WalkState);
    public bool CanChangeWeapon => StateMachine.CurrentState == IdleState;
    public bool IsDead => StateMachine.CurrentState == DeadState;
    public bool HeadCrashed => headCrashCheck.IsCrashed;

    public void OnLandingAnimEnd() => LandingState.NotifyLandingEnd();
    public void OnJumpingAnimEnd() => JumpState.NotifyJumpingEnd();

    private void Awake()
    {
        StateMachine = new StateMachine();

        IdleState = new PlayerIdleState(this, StateMachine);
        WalkState = new PlayerWalkState(this, StateMachine);
        RunState = new PlayerRunState(this, StateMachine);
        CrouchState = new PlayerCrouchState(this, StateMachine);
        CrouchWalkState = new PlayerCrouchWalkState(this, StateMachine);
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