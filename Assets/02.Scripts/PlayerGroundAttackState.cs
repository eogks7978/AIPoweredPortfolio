using UnityEngine;

// --- GROUND ATTACK STATE ---
public class PlayerGroundAttackState : PlayerGroundedState
{
    private bool nextAttackBuffered;   // 공격 중 입력 버퍼
    private int comboIndex;            // 현재 콤보 인덱스
    private bool attackEnded;          // 애니메이션 종료 플래그 (애니메이션 이벤트로 세팅)

    private const int MaxComboIndex = 2; // 0, 1, 2 → 3콤보

    public PlayerGroundAttackState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        comboIndex = 0;
        nextAttackBuffered = false;
        attackEnded = true;

        playerController.player.Rb.linearVelocity 
            = new Vector3(0f, playerController.player.Rb.linearVelocity.y, 0f);

        //PlayCurrentAttack();
    }

    public override void Update()
    {
        // 공격 중 마우스 클릭 → 버퍼에만 저장, 즉시 전환 X
        if (playerController.player.PlayerInput.AttackPressed)
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
                stateMachine.ChangeState(playerController.IdleState);
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void PlayCurrentAttack()
    {
        // IWeapon.GetNormalAttack()으로 해당 콤보의 ISkill을 가져와 애니메이션 재생
        ISkill skill = playerController.player.WeaponController.CurrentWeapon.GetNormalAttack(comboIndex);

        if (skill != null)
        {
            playerController.player.Anim.Play(skill.Data.AnimationName);  // ISkill에 AnimationName 프로퍼티 필요
        }

        // 실제 데미지/이펙트는 IWeapon.Attack() 또는 ISkill.Execute()로 위임
        playerController.player.WeaponController.CurrentWeapon.Attack();
    }
}