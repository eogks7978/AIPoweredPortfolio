using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "Skills/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("기본 정보")]
    public int SkillID;
    public string SkillName;
    [TextArea] public string Description;
    public Sprite Icon;

    [Header("전투 수치")]
    public float DamageMultiplier;
    public float Range;
    public float Cooldown;
    public int ManaCost;
    public int ComboCount;

    [Header("연출")]
    public string AnimationName;
    public string[] ComboAnimationNames;
    public GameObject VfxPrefab;
}