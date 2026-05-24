public interface IWeapon
{
    ISkill GetNormalAttack(int comboIndex);
    ISkill GetSkill(int skillSlot);

    void Attack(); // public 붙이거나 안붙이거나 인터페이스에서는 동일
}