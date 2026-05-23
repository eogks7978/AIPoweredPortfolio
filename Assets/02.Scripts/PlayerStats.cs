using UnityEngine;

public class PlayerStats
{
    [Header("Movement Stats")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float attackPoint = 50f;
    [SerializeField] private float attackSpeed = 5f;

    // 외부(State 등)에서 읽을 수 있도록 프로퍼티 제공
    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    public float JumpForce => jumpForce;
    public float AttackPoint => attackPoint;
    public float AttackSpeed => attackSpeed;
}