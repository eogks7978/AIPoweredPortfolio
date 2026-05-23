public interface ISkill
{
    string SkillName { get; }
    string AnimationName { get; }
    float CoolTime { get; }

    bool CanUse(PlayerController player); // 쿨타임이나 마나 체크
    void Execute(PlayerController player); // 실제 스킬 발동 로직
}