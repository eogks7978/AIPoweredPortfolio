public interface IWeapon
{
    string IdleAnimationName { get; }    // 💡 각 무기가 자기 대기 애니메이션 이름을 알려줌
    string WalkAnimationName { get; }    // 💡 각 무기가 자기 대기 애니메이션 이름을 알려줌
    string RunAnimationName { get; }    // 💡 각 무기가 자기 대기 애니메이션 이름을 알려줌

    ISkill GetNormalAttack(int comboIndex);
    ISkill GetSkill(int skillSlot);

    void Attack(); // public 붙이거나 안붙이거나 인터페이스에서는 동일
}