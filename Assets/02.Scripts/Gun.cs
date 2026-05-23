using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    public string IdleAnimationName => "Idle_Rifle";

    public string WalkAnimationName => "Walk_Rifle";

    public string RunAnimationName => "Run_Rifle";

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
