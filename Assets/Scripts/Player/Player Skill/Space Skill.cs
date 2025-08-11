using System.Collections;
using UnityEngine;

public class SpaceSkill : Skill
{
    protected override void Awake()
    {
        base.Awake();
        code = 1;
        // level = 0;
        // cooltime = 10f;
        // damage = 1.5f;
        // cost = 10f;
        // duration = 0f;
        maxLevel = 5;
    }

    protected override void Start()
    {
        LoadSkill();
        SaveSkill();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanActivateSkill()
    {
        return base.CanActivateSkill();
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
            cooltime -= 1f;
            damage += 0.1f;
            cost -= 1;
            timer = cooltime;
        }

        SaveSkill();
    }

    public override void SaveSkill()
    {
        PlayerPrefs.SetInt(this.code + " Skill Level", this.level);
        PlayerPrefs.SetFloat(this.code + " Skill Cooltime", this.cooltime);
        PlayerPrefs.SetFloat(this.code + " Skill Damage", this.damage);
        PlayerPrefs.SetFloat(this.code + " Skill Cost", this.cost);
        PlayerPrefs.SetFloat(this.code + " Skill Duration", this.duration);
        PlayerPrefs.SetFloat(this.code + " Skill Timer", this.timer);
    }

    public override void LoadSkill()
    {
        this.level = PlayerPrefs.HasKey(this.code + " Skill Level") ? PlayerPrefs.GetInt(this.code + " Skill Level", this.level) : 0;
        this.cooltime = PlayerPrefs.HasKey(this.code + " Skill Cooltime") ? PlayerPrefs.GetFloat(this.code + " Skill Cooltime", this.cooltime) : 10f;
        this.damage = PlayerPrefs.HasKey(this.code + " Skill Damage") ? PlayerPrefs.GetFloat(this.code + " Skill Damage", this.damage) : 1.2f;
        this.cost = PlayerPrefs.HasKey(this.code + " Skill Cost") ? PlayerPrefs.GetFloat(this.code + " Skill Cost", this.cost) : 10f;
        this.duration = PlayerPrefs.HasKey(this.code + " Skill Duration") ? PlayerPrefs.GetFloat(this.code + " Skill Duration", this.duration) : 0f;
        this.timer = PlayerPrefs.HasKey(this.code + " Skill Timer") ? PlayerPrefs.GetFloat(this.code + " Skill Timer") : cooltime;
    }

    protected override void SkillTimer()
    {
        base.SkillTimer();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        SoundManager.Instance.Play2DSound("Space Skill Sound");
        Debug.Log("SpaceSkill");
    }

    public override SkillHitBox HitBox => this.hitBox;
}
