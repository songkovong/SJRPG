using System.Collections;
using UnityEngine;

public class CSkill : Skill
{
    float durationTimer;
    public bool isDuration { get; private set; }
    Player player;
    public GameObject effect;

    protected override void Awake()
    {
        base.Awake();
        code = 2;
        // level = 0;
        // cooltime = 20f;
        // damage = 0.1f;
        // cost = 20f;
        // duration = 6f;
        maxLevel = 10;
    }

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();

        LoadSkill();

        SaveSkill();
    }

    protected override void Update()
    {
        base.Update();
        SkillDuration();
    }

    public void SkillDuration()
    {
        if (isDuration == true)
        {
            effect.SetActive(true);
            durationTimer += Time.deltaTime;
            if (durationTimer >= duration)
            {
                isDuration = false;
            }
        }
        else
        {
            effect.SetActive(false);

        }
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
            duration++;
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
        this.damage = PlayerPrefs.HasKey(this.code + " Skill Damage") ? PlayerPrefs.GetFloat(this.code + " Skill Damage", this.damage) : 0.1f;
        this.cost = PlayerPrefs.HasKey(this.code + " Skill Cost") ? PlayerPrefs.GetFloat(this.code + " Skill Cost", this.cost) : 20f;
        this.duration = PlayerPrefs.HasKey(this.code + " Skill Duration") ? PlayerPrefs.GetFloat(this.code + " Skill Duration", this.duration) : 8f;

        timer = cooltime;
    }

    protected override void SkillTimer()
    {
        base.SkillTimer();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("CSkill = " + canSkill);
        durationTimer = 0;
        isDuration = true;
        StartCoroutine(player.playerStat.BuffDamage());
    }

    public override SkillHitBox HitBox => this.hitBox;
}
