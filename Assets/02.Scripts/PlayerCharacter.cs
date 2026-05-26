using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Rigidbody Rb { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerController Controller { get; private set; }
    public PlayerInputHandler PlayerInput { get; private set; }
    public SkillController Skill { get; private set; }
    public WeaponController WeaponController { get; private set; }
    public PlayerStats Stats { get; private set; }

    public float CurrentMoveSpeed { get; set; }
    public float maxMoveSpeed { get; set; }

    public Sword tempWeapon;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        Controller = GetComponent<PlayerController>();
        PlayerInput = GetComponent<PlayerInputHandler>();
        Skill = GetComponent<SkillController>();
        WeaponController = GetComponent<WeaponController>();

        Rb.constraints = RigidbodyConstraints.FreezeRotation;

        Stats = new PlayerStats();

        Stats = new PlayerStats();
        Stats.attackSpeed = 5f;
        Stats.attackPoint = 5f;
        Stats.walkSpeed = 3f;
        Stats.runSpeed = 6f;
        Stats.jumpForce = 5f;

        maxMoveSpeed = 6;
    }

    private void Start()
    {
        WeaponController.ChangeWeapon(tempWeapon);
        Controller.StateMachine.ChangeState(Controller.IdleState);
    }

    public Vector3 GetMoveDirection()
    {
        float moveX = PlayerInput.MoveInput.x;
        float moveZ = PlayerInput.MoveInput.y;

        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        return (forward * moveZ + right * moveX).normalized;
    }

    public void Move(Vector3 velocity)
    {
        if (!Controller.IsGrounded) return;

        Vector3 horizontal = new Vector3(velocity.x, 0f, velocity.z);

        if (horizontal.magnitude > maxMoveSpeed)
        {
            horizontal = horizontal.normalized * maxMoveSpeed;
        }

        Rb.linearVelocity = new Vector3(horizontal.x, Rb.linearVelocity.y, horizontal.z);

        Rotation(velocity);
    }

    public void Rotation(Vector3 moveDir)
    {
        if (moveDir != Vector3.zero)
        {
            moveDir.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.fixedDeltaTime * 10f
            );
        }
    }

    // 式式 餌蜂 籀葬 式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    private void Die()
    {
        Controller.StateMachine.ChangeState(Controller.DeadState);
    }
}