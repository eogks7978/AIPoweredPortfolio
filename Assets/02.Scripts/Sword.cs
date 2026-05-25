using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Attack!");
    }

    public ISkill GetNormalAttack(int comboIndex)
    {
        Debug.Log("기본공격!");
        return null;
    }

    public ISkill GetSkill(int skillSlot)
    {
        Debug.Log("GetSkill!");
        return null;
    }
}
