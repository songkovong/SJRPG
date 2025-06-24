using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Active Skill Data")]
public class ActiveSkillData : SkillData
{
    public float baseCooltime;
    public float baseDamage;
    public float baseCost;
    public float baseDuration;
    public int maxLevel = 5;
}
