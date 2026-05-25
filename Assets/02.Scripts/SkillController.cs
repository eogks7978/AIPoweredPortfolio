using UnityEngine;
using UnityEngine.InputSystem;

public class SkillController : MonoBehaviour
{
    private PlayerController playerController;

    // 슬롯별 쿨다운 잔여 시간 (0이면 사용 가능)
    private float[] cooldownTimers;
    private const int SkillSlotCount = 4; // Q, E, R, 궁 등

    public PlayerInputHandler playerInputHandler { get; private set; }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        cooldownTimers = new float[SkillSlotCount];
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        // 쿨다운 카운트다운
        for (int i = 0; i < cooldownTimers.Length; i++)
        {
            if (cooldownTimers[i] > 0f)
                cooldownTimers[i] -= Time.deltaTime;
        }

        HandleSkillInput();
    }

    // ──────────────────────────────────────
    // 스킬 슬롯 입력 처리 (Q, E, R ...)
    // ──────────────────────────────────────
    private void HandleSkillInput()
    {
        // 예시: Q키 → slot 0, E키 → slot 1
        if (Keyboard.current.qKey.wasPressedThisFrame) TryUseSkill(0);
        if (Keyboard.current.eKey.wasPressedThisFrame) TryUseSkill(1);
    }

    // ──────────────────────────────────────
    // 일반 공격 (콤보) 실행
    // PlayerGroundAttackState에서 호출
    // ──────────────────────────────────────
    public void ExecuteNormalAttack(int comboIndex)
    {
        if (playerController.player.WeaponController.CurrentWeapon == null) return;

        ISkill skill = playerController.player.WeaponController.CurrentWeapon.GetNormalAttack(comboIndex);
        ExecuteSkill(skill);
    }

    // ──────────────────────────────────────
    // 슬롯 스킬 실행 시도
    // ──────────────────────────────────────
    public bool TryUseSkill(int slot)
    {
        if (!CanUseSkill(slot)) return false;
        if (playerController.player.WeaponController.CurrentWeapon == null) return false;

        ISkill skill = playerController.player.WeaponController.CurrentWeapon.GetSkill(slot);
        if (skill == null) return false;

        ExecuteSkill(skill);
        cooldownTimers[slot] = skill.Data.Cooldown; // ISkill.Data에 Cooldown 필요
        return true;
    }

    public bool CanUseSkill(int slot) => cooldownTimers[slot] <= 0f;

    public float GetCooldownRatio(int slot)
    {
        ISkill skill = playerController.player.WeaponController.CurrentWeapon?.GetSkill(slot);
        if (skill == null || skill.Data.Cooldown <= 0f) return 0f;
        return cooldownTimers[slot] / skill.Data.Cooldown;
    }

    // ──────────────────────────────────────
    // 실제 스킬 발동 (애니메이션 + 로직)
    // ──────────────────────────────────────
    private void ExecuteSkill(ISkill skill)
    {
        if (skill == null) return;

        // 애니메이션
        playerController.player.Anim.Play(skill.Data.AnimationName);

        // 데미지/이펙트/이동 등 스킬 고유 로직
        skill.Execute(playerController.player.gameObject, playerController.player.PlayerInput.CurrentTarget);
    }
}