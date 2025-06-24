using UnityEngine;

public class AttackMasterySkill : Skill
{
    public float masteryStat;
    protected override void Awake()
    {
        base.Awake();
        code = 4;
        maxLevel = 20;

        LoadSkill();
    }

    protected override void Start()
    {
        SaveSkill();

        canSkill = false;
    }

    public override void SaveSkill()
    {
        PlayerPrefs.SetInt(this.code + " Skill Level", this.level);
        PlayerPrefs.SetFloat(this.code + " Skill Cooltime", this.cooltime);
        PlayerPrefs.SetFloat(this.code + " Skill Damage", this.damage);
        PlayerPrefs.SetFloat(this.code + " Skill Cost", this.cost);
        PlayerPrefs.SetFloat(this.code + " Skill Duration", this.duration);
        PlayerPrefs.SetFloat(this.code + " Skill Stat", this.masteryStat);
    }

    public override void LoadSkill()
    {
        this.level = PlayerPrefs.HasKey(this.code + " Skill Level") ? PlayerPrefs.GetInt(this.code + " Skill Level", this.level) : 0;
        this.cooltime = PlayerPrefs.HasKey(this.code + " Skill Cooltime") ? PlayerPrefs.GetFloat(this.code + " Skill Cooltime", this.cooltime) : 0;
        this.damage = PlayerPrefs.HasKey(this.code + " Skill Damage") ? PlayerPrefs.GetFloat(this.code + " Skill Damage", this.damage) : 0;
        this.cost = PlayerPrefs.HasKey(this.code + " Skill Cost") ? PlayerPrefs.GetFloat(this.code + " Skill Cost", this.cost) : 0;
        this.duration = PlayerPrefs.HasKey(this.code + " Skill Duration") ? PlayerPrefs.GetFloat(this.code + " Skill Duration", this.duration) : 0;
        this.masteryStat = PlayerPrefs.HasKey(this.code + " Skill Stat") ? PlayerPrefs.GetFloat(this.code + " Skill Stat", this.masteryStat) : 0;
    }

    public override void SkillLevelUp()
    {
        level++;
        masteryStat += 0.02f;

        SaveSkill();
    }
}
