using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool RunHeld { get; private set; }

    // 타겟
    public GameObject CurrentTarget { get; private set; }

    [SerializeField] private LayerMask targetLayer;   // Enemy 레이어
    [SerializeField] private float maxRayDistance = 50f;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        MoveInput = new Vector2(
            Keyboard.current.dKey.isPressed ? 1 : Keyboard.current.aKey.isPressed ? -1 : 0,
            Keyboard.current.wKey.isPressed ? 1 : Keyboard.current.sKey.isPressed ? -1 : 0
        );

        JumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        RunHeld = Keyboard.current.leftShiftKey.isPressed;

        HandleTargetClick();

        // 타겟이 있을 때만 공격 입력 수리
        AttackPressed = CurrentTarget != null
                        && Mouse.current.leftButton.wasPressedThisFrame;
    }

    private void HandleTargetClick()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        // 에디터에서 Ray 시각화 (5초간 표시)
        Debug.DrawRay(ray.origin, ray.direction * maxRayDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, targetLayer))
        {
            // 맞은 지점까지만 다른 색으로 표시
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 5f);
            SetTarget(hit.collider.gameObject);
            return;
        }

        ClearTarget();
    }

    public void SetTarget(GameObject target)
    {
        CurrentTarget = target;
        Debug.Log($"[Target] {target.name} 타겟 설정");
    }

    public void ClearTarget()
    {
        if (CurrentTarget == null) return;
        Debug.Log($"[Target] 타겟 해제");
        CurrentTarget = null;
    }
}