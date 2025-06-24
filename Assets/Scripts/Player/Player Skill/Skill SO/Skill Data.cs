using UnityEngine;

public enum SkillType { Active, Pasive }
public abstract class SkillData : ScriptableObject
{
    public int code;
    public int skillName;
    public int decription;
    public SkillType type;
    public Sprite icon;
}
