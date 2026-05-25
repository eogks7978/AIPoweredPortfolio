using UnityEngine;

public interface ISkill
{
    SkillData Data { get; }

    // 1단계: 지금 스킬을 발동할 수 있는 조건(사거리, 타겟 유무 등)인지 체크하는 메서드
    bool CanExecute(GameObject caster, GameObject target);

    // 2단계: 조건을 통과했을 때 실제로 애니메이션을 틀고 효과를 내는 메서드
    void Execute(GameObject caster, GameObject target = null);
}