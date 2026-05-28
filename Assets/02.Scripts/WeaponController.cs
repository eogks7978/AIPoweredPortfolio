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

    public void ChangeWeapon(IWeapon newWeapon)
    {
        if (player.StateController.StateMachine.CurrentState == player.StateController.IdleState) return;

        CurrentWeapon = newWeapon;
        ApplyAnimatorOverride(newWeapon);
    }

    private void ApplyAnimatorOverride(IWeapon weapon)
    {
        player.Anim.runtimeAnimatorController = weapon is Sword ? swordOverride : gunOverride;
        player.Anim.Play("Idle");
    }
}