using System.Collections;
using UnityEngine;

public class RSkill : Skill
{
    public float waitSec;
    public float effectDuration;
    public GameObject effect;

    protected override void Awake()
    {
        base.Awake();
        code = 3;
        // level = 0;
        // cooltime = 30f; //30
        // damage = 2f;
        // cost = 30f; //30
        // duration = 0f;
        maxLevel = 5;
        LoadSkill();
    }

    protected override void Start()
    {
        base.Start();
        effect.SetActive(false);

        SaveSkill();
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log("Level = " + level);
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
            cooltime -= 2f;
            damage += 0.1f;
            cost -= 3f;
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
    }

    public override void LoadSkill()
    {
        this.level = PlayerPrefs.HasKey(this.code + " Skill Level") ? PlayerPrefs.GetInt(this.code + " Skill Level", this.level) : 0;
        this.cooltime = PlayerPrefs.HasKey(this.code + " Skill Cooltime") ? PlayerPrefs.GetFloat(this.code + " Skill Cooltime", this.cooltime) : 30f;
        this.damage = PlayerPrefs.HasKey(this.code + " Skill Damage") ? PlayerPrefs.GetFloat(this.code + " Skill Damage", this.damage) : 1.5f;
        this.cost = PlayerPrefs.HasKey(this.code + " Skill Cost") ? PlayerPrefs.GetFloat(this.code + " Skill Cost", this.cost) : 30f;
        this.duration = PlayerPrefs.HasKey(this.code + " Skill Duration") ? PlayerPrefs.GetFloat(this.code + " Skill Duration", this.duration) : 0f;

        timer = cooltime;
    }

    protected override void SkillTimer()
    {
        base.SkillTimer();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        StartCoroutine(EffectCoroutine(waitSec, effectDuration));
        Debug.Log("RSkill");
    }

    IEnumerator EffectCoroutine(float waitSec, float duration)
    {
        yield return new WaitForSeconds(waitSec);
        effect.SetActive(true);
        yield return new WaitForSeconds(duration);
        effect.SetActive(false);
    }

    public override SkillHitBox HitBox => this.hitBox;
}
