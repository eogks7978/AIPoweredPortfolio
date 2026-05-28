using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCharacter : MonoBehaviour
{
    public Animator Anim { get; private set; }
    public PlayerStateController StateController { get; private set; }
    public PlayerInputHandler PlayerInput { get; private set; }
    public ExampleCharacterController MovingController { get; private set; }
    public SkillController Skill { get; private set; }
    public WeaponController WeaponController { get; private set; }
    public PlayerStats Stats { get; private set; }
    public float maxMoveSpeed { get; set; }

    public Sword tempWeapon;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        StateController = GetComponent<PlayerStateController>();
        PlayerInput = GetComponent<PlayerInputHandler>();
        MovingController = GetComponent<ExampleCharacterController>();
        Skill = GetComponent<SkillController>();
        WeaponController = GetComponent<WeaponController>();

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
        StateController.StateMachine.ChangeState(StateController.IdleState);
    }

    // 式式 餌蜂 籀葬 式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    private void Die()
    {
        StateController.StateMachine.ChangeState(StateController.DeadState);
    }
}