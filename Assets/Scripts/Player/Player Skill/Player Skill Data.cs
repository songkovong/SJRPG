using UnityEngine;

[CreateAssetMenu(fileName = "Player Skill Data", menuName = "Player Data/Player Skill Data")]
public class PlayerSkillData : ScriptableObject
{
    public int code;
    public float cooltimeData;
    public float damageData;
    public float magicData;
}
