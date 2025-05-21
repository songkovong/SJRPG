using UnityEngine;

[CreateAssetMenu(fileName = "Player Skill Data", menuName = "Player Data/Player Skill Data")]
public class PlayerSkillData : ScriptableObject
{
    public int skillCode;
    public float cooltimeData;
    public float damageData;
}
