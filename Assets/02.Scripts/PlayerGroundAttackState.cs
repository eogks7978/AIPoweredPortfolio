using UnityEngine;

// --- GROUND ATTACK STATE ---
public class PlayerGroundAttackState : PlayerGroundedState
{
    private bool nextAttackBuffered;   // 공격 중 입력 버퍼
    private int comboIndex;            // 현재 콤보 인덱스
    private bool attackEnded;          // 애니메이션 종료 플래그 (애니메이션 이벤트로 세팅)

    // IWeapon.GetNormalAttack()에서 콤보 최대치를 받아오기 위한 상수
    // 실제 무기별 최대 콤보는 ISkill / IWeapon 쪽에서 관리하는 것을 권장
    private const int MaxComboIndex = 2; // 0, 1, 2 → 3콤보

    public PlayerGroundAttackState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    // ─────────────────────────────────────────
    // Enter : 상태 진입 시 초기화 후 첫 공격
    // ─────────────────────────────────────────
    public override void Enter()
    {
        // 💡 base.Enter()는 의도적으로 호출하지 않습니다.
        //    PlayerGroundedState.Update()에 공격 전환 로직이 있으므로
        //    base.Update()도 아래 Update에서 선택적으로만 호출합니다.

        comboIndex = 0;
        nextAttackBuffered = false;
        attackEnded = false;

        PlayCurrentAttack();
    }

    // ─────────────────────────────────────────
    // Update : 버퍼 입력 수집 + 종료 감지
    // ─────────────────────────────────────────
    public override void Update()
    {
        // 💡 부모(PlayerGroundedState)의 Update는 호출하지 않습니다.
        //    호출하면 Mouse0 입력 시 또 ChangeState(GroundAttackState)를 타기 때문입니다.
        //    단, 점프와 낙하 감지는 여기서 직접 처리합니다.

        // 공격 중 마우스 클릭 → 버퍼에만 저장, 즉시 전환 X
        if (player.playerInputHandler.AttackPressed)
        {
            nextAttackBuffered = true;
        }

        // 애니메이션 이벤트(OnAttackAnimEnd)가 호출된 뒤 처리
        if (attackEnded)
        {
            attackEnded = false;

            if (nextAttackBuffered && comboIndex < MaxComboIndex)
            {
                // 다음 콤보로 이어감 (Idle 경유 없음)
                comboIndex++;
                nextAttackBuffered = false;
                PlayCurrentAttack();
            }
            else
            {
                // 콤보 끝 → Idle로 복귀
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void FixedUpdate()
    {
        player.Rb.linearVelocity = new Vector3(0f, player.Rb.linearVelocity.y, 0f);
    }

    // ─────────────────────────────────────────
    // Exit
    // ─────────────────────────────────────────
    public override void Exit()
    {
        base.Exit();
        nextAttackBuffered = false;
        attackEnded = false;
    }

    // ─────────────────────────────────────────
    // 애니메이션 이벤트에서 호출 (Animation Event)
    // PlayerController를 통해 호출하거나, Animator의 이벤트 함수로 연결하세요.
    // 예) PlayerController에 public void OnAttackAnimEnd() => GroundAttackState.NotifyAttackEnd(); 추가
    // ─────────────────────────────────────────
    public void NotifyAttackEnd()
    {
        attackEnded = true;
    }

    // ─────────────────────────────────────────
    // 현재 콤보 인덱스에 맞는 공격 실행
    // ─────────────────────────────────────────
    private void PlayCurrentAttack()
    {
        // IWeapon.GetNormalAttack()으로 해당 콤보의 ISkill을 가져와 애니메이션 재생
        ISkill skill = player.CurrentWeapon.GetNormalAttack(comboIndex);

        if (skill != null)
        {
            player.Anim.Play(skill.AnimationName);  // ISkill에 AnimationName 프로퍼티 필요
        }

        // 실제 데미지/이펙트는 IWeapon.Attack() 또는 ISkill.Execute()로 위임
        player.CurrentWeapon.Attack();
    }
}