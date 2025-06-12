using System.Collections;
using UnityEngine;

public class CSkill : Skill
{
    float durationTimer;
    bool isDuration;
    Player player;
    public GameObject effect;

    protected override void Awake()
    {
        base.Awake();
        code = 2;
        level = 1;
        cooltime = 20f;
        damage = 0f;
        cost = 20f;
        duration = 6f;
        maxLevel = 5;
    }

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
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
        if (level < maxLevel)
        {
            level++;
            cooltime -= 2;
            cost -= 4;
            duration++;
        }
    }

    protected override void SaveSkill()
    {
        base.SaveSkill();
    }

    protected override void LoadSkill()
    {
        base.LoadSkill();
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
