using UnityEngine;

public class GuardSkill : Skill
{
    public float masteryStat;
    protected override void Awake()
    {
        base.Awake();
        code = 5;
        maxLevel = 20;

        playerStat = GetComponent<PlayerStat>();
    }

    protected override void Start()
    {
        LoadSkill();

        SaveSkill();

        if (level == 0) canSkill = false;
        else canSkill = true;
    }

    protected override void Update()
    {
        SkillTimer();
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
        this.cooltime = PlayerPrefs.HasKey(this.code + " Skill Cooltime") ? PlayerPrefs.GetFloat(this.code + " Skill Cooltime", this.cooltime) : 2f;
        this.damage = PlayerPrefs.HasKey(this.code + " Skill Damage") ? PlayerPrefs.GetFloat(this.code + " Skill Damage", this.damage) : 0;
        this.cost = PlayerPrefs.HasKey(this.code + " Skill Cost") ? PlayerPrefs.GetFloat(this.code + " Skill Cost", this.cost) : 0;
        this.duration = PlayerPrefs.HasKey(this.code + " Skill Duration") ? PlayerPrefs.GetFloat(this.code + " Skill Duration", this.duration) : 0;
        this.masteryStat = PlayerPrefs.HasKey(this.code + " Skill Stat") ? PlayerPrefs.GetFloat(this.code + " Skill Stat", this.masteryStat) : 2f;
    }
    
    public override void SkillLevelUp()
    {
        if (level == 0)
        {
            level++;
        }
        else
        {
            level++;
            masteryStat -= 0.05f;
        }

        canSkill = true;

        SaveSkill();
    }

    public override bool CanActivateSkill()
    {
        // return base.CanActivateSkill();
        return (playerStat.data.currentMagic >= masteryStat) && canSkill && !IsLevel0();
    }
}
