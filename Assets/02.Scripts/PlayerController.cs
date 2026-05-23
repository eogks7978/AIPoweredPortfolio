using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

    public bool IsGrounded => Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundLayer);

    public Rigidbody Rb { get; private set; }
    public Animator Anim { get; private set; }

    // FSM 핵심 인스턴스들
    public StateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerGroundedState GroundState { get; private set; }
    public PlayerGroundAttackState GroundAttackState { get; private set; }
    public PlayerAirborneState AirborneState { get; private set; }
    public PlayerAirAttackState AirAttackState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }

	public PlayerStats Stats { get; private set; }
    public IWeapon CurrentWeapon { get; private set; }
    public void OnAttackAnimEnd() => GroundAttackState.NotifyAttackEnd();


    public bool IsMoving => Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    public bool CanJump => IsGrounded && StateMachine.CurrentState is PlayerGroundedState && StateMachine.CurrentState != GroundAttackState;
    public bool CanRun => StateMachine.CurrentState == IdleState || StateMachine.CurrentState == WalkState;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();

        Rb.constraints = RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationZ;

        Anim = GetComponent<Animator>();

        StateMachine = new StateMachine();

        IdleState = new PlayerIdleState(this, StateMachine);
        WalkState = new PlayerWalkState(this, StateMachine);
        RunState = new PlayerRunState(this, StateMachine);
        GroundAttackState = new PlayerGroundAttackState(this, StateMachine);
        AirAttackState = new PlayerAirAttackState(this, StateMachine);
        JumpState = new PlayerJumpState(this, StateMachine);
        FallState = new PlayerFallState(this, StateMachine);
        DeadState = new PlayerDeadState(this, StateMachine);

		Stats = new PlayerStats();
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
        CurrentWeapon = newWeapon;

        // 💡 만약 플레이어가 현재 가만히 서 있는 상태(Idle)에서 무기를 바꿨다면?
        if (StateMachine.CurrentState == IdleState)
        {
            // Idle 애니메이션을 새 무기 버전으로 즉시 갈아끼워 줍니다!
            Anim.Play(CurrentWeapon.IdleAnimationName);
        }
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