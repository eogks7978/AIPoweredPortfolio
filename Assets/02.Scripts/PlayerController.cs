using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler { get; private set; }
    public Rigidbody Rb { get; private set; }
    public Animator Anim { get; private set; }

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
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

    public PlayerStats Stats { get; private set; }
    public IWeapon CurrentWeapon { get; private set; }
    public float CurrentMoveSpeed { get; set; }

    public bool IsMoving => playerInputHandler.MoveInput != Vector2.zero;
    public bool CanJump => IsGrounded && StateMachine.CurrentState is PlayerGroundedState && StateMachine.CurrentState != GroundAttackState;
    public bool CanRun => StateMachine.CurrentState == IdleState || StateMachine.CurrentState == WalkState;
    public bool CanChangeWeapon => StateMachine.CurrentState == IdleState;

    public void OnJumpForce() => Rb.AddForce(Vector3.up * Stats.jumpForce, ForceMode.Impulse);
    public void OnLandingAnimEnd() => LandingState.NotifyLandingEnd();
    public void OnAttackAnimEnd() => GroundAttackState.NotifyAttackEnd();
    public bool IsGrounded => Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundLayer);

    public Gun tempWeapon;

    private void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();

        Rb = GetComponent<Rigidbody>();

        Rb.constraints = RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationZ;

        Anim = GetComponent<Animator>();

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

		Stats = new PlayerStats();
        Stats.attackSpeed = 5f;
        Stats.attackPoint = 5f;
        Stats.walkSpeed = 3f;
        Stats.runSpeed = 6f;
        Stats.jumpForce = 5f;

        groundLayer = LayerMask.GetMask("Ground");

        StateMachine.ChangeState(IdleState);

        ChangeWeapon(tempWeapon);
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

    public void ChangeWeapon(IWeapon newWeapon)
    {
        if (CanChangeWeapon)
        {
            CurrentWeapon = newWeapon;

            // Idle 애니메이션을 새 무기 버전으로 즉시 갈아끼워 줍니다!
            Anim.runtimeAnimatorController = newWeapon is Sword ? swordOverride : gunOverride;
            Anim.Play("Idle");
        }
    }

    public Vector3 GetMoveDirection()
    {
        float moveX = playerInputHandler.MoveInput.x;
        float moveZ = playerInputHandler.MoveInput.y;

        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        return (forward * moveZ + right * moveX).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
        }
    }
}