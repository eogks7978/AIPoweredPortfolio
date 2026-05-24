using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
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
