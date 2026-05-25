using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public IWeapon CurrentWeapon { get; private set; }

    [SerializeField] private AnimatorOverrideController swordOverride;
    [SerializeField] private AnimatorOverrideController gunOverride;

    private PlayerCharacter player;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
    }

    public bool CanChangeWeapon =>
        player.Controller.StateMachine.CurrentState == player.Controller.IdleState;

    public void ChangeWeapon(IWeapon newWeapon)
    {
        if (!CanChangeWeapon) return;

        CurrentWeapon = newWeapon;
        ApplyAnimatorOverride(newWeapon);
    }

    private void ApplyAnimatorOverride(IWeapon weapon)
    {
        player.Anim.runtimeAnimatorController = weapon is Sword ? swordOverride : gunOverride;
        player.Anim.Play("Idle");
    }
}