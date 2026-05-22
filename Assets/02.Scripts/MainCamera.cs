using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("추적 대상 설정")]
    public Transform target;            // 바라볼 캐릭터
    public Vector3 targetOffset = new Vector3(0f, 1.5f, 0f); // 캐릭터 중심점 보정 (시선/레이 기준점)

    [Header("마우스 회전 설정")]
    public float mouseSensitivity = 200f; // 마우스 감도
    public float minVerticalAngle = -20f; // 아래로 내려다볼 수 있는 최대 각도 (땅 뚫기 방지)
    public float maxVerticalAngle = 70f;  // 위로 올려다볼 수 있는 최대 각도 (뒤집힘 방지)

    [Header("카메라 거리 설정")]
    public float defaultDistance = 5f;    // 캐릭터와의 기본 유지 거리
    public float minDistance = 1f;        // 장애물로 인해 당겨질 수 있는 최소 거리

    [Header("부드러운 이동/회전 속도")]
    public float positionSmoothTime = 0.15f; // 이동 부드러움
    public float rotationSmoothTime = 0.05f; // 회전 부드러움

    [Header("장애물 감지 설정")]
    public LayerMask obstacleLayer;       // 장애물 레이어 (Player 레이어 제외 필수)
    public float cameraRadius = 0.25f;     // 카메라 두께 (벽 뚫림 방지 구체 반경)

    // 내부 연산용 변수들
    private float _mouseX;
    private float _mouseY;
    private float _currentDistance;
    private Vector3 _smoothPositionVelocity;
    private float _smoothDistanceVelocity;
    private Quaternion _targetRotation;

    void Start()
    {
        // 1. 마우스 회전 초기값 설정 (현재 카메라의 회전값 기준)
        Vector3 angles = transform.eulerAngles;
        _mouseX = angles.y;
        _mouseY = angles.x;

        _currentDistance = defaultDistance;

        // 게임 화면 클릭 시 마우스 커서를 숨기고 중앙에 고정 (원치 않으시면 삭제 가능)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // [수정된 2번 영역] 새 인풋 시스템 방식으로 마우스 델타값 가져오기
        Vector2 mouseDelta = Vector2.zero;
        if (UnityEngine.InputSystem.Mouse.current != null)
        {
            // 구형 Input.GetAxis와 유사한 감도를 맞추기 위해 대략 0.05f 정도를 곱해 보정합니다.
            mouseDelta = UnityEngine.InputSystem.Mouse.current.delta.ReadValue() * 0.05f;
        }

        _mouseX += mouseDelta.x * mouseSensitivity * Time.deltaTime;
        _mouseY -= mouseDelta.y * mouseSensitivity * Time.deltaTime;

        // 3. 카메라 뒤집힘 및 땅 파고들기 방지 (이하 코드는 기존과 동일)
        _mouseY = Mathf.Clamp(_mouseY, minVerticalAngle, maxVerticalAngle);

        // 4. 목표 회전값 계산 및 부드러운 보간
        Quaternion desiredRotation = Quaternion.Euler(_mouseY, _mouseX, 0f);
        _targetRotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothTime / Time.deltaTime);
        transform.rotation = _targetRotation;

        // 5. 시선 및 레이캐스트의 시작점이 될 캐릭터의 실제 중심점 계산
        Vector3 rayOrigin = target.position + targetOffset;

        // 6. 회전값에 따른 카메라의 '원래 있어야 할 목표 위치' 계산
        Vector3 targetDirection = _targetRotation * Vector3.back; // 카메라가 바라보는 방향의 반대(뒤쪽)
        Vector3 idealPosition = rayOrigin + targetDirection * defaultDistance;

        // 7. SphereCast로 캐릭터 중심에서 idealPosition 사이에 장애물이 있는지 체크
        RaycastHit hit;
        float desiredDistance = defaultDistance;

        // 카메라 반지름을 고려하여 구체 레이를 발사
        if (Physics.SphereCast(rayOrigin, cameraRadius, targetDirection, out hit, defaultDistance, obstacleLayer))
        {
            // 장애물에 부딪혔다면 충돌 지점만큼 거리를 제한 (최소 거리 보장)
            desiredDistance = Mathf.Clamp(hit.distance - cameraRadius, minDistance, defaultDistance);
        }

        // 거리를 부드럽게 줄이거나 늘림 (Damp 처리로 튕김 방지)
        _currentDistance = Mathf.SmoothDamp(_currentDistance, desiredDistance, ref _smoothDistanceVelocity, positionSmoothTime);

        // 8. 최종 위치 세팅 및 부드러운 이동 반영
        Vector3 finalTargetPosition = rayOrigin + targetDirection * _currentDistance;
        transform.position = Vector3.SmoothDamp(transform.position, finalTargetPosition, ref _smoothPositionVelocity, positionSmoothTime);
    }
}
