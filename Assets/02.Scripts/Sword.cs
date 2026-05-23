using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public string IdleAnimationName => "Idle_Sword";

    public string WalkAnimationName => "Walk_Sword";

    public string RunAnimationName => "Run_Sword";

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public ISkill GetNormalAttack(int comboIndex)
    {
        throw new System.NotImplementedException();
    }

    public ISkill GetSkill(int skillSlot)
    {
        throw new System.NotImplementedException();
    }
}
