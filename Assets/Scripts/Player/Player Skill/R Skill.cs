using System.Collections;
using UnityEngine;

public class RSkill : Skill
{
    protected override void Awake()
    {
        base.Awake();
        code = 3;
        level = 1;
        cooltime = 30f;
        damage = 3f;
        cost = 30f;
        duration = 0f;
        maxLevel = 5;
    }

    protected override void Start()
    {
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
        if (level < maxLevel)
        {
            level++;
            cooltime -= 2f;
            damage += 0.5f;
            cost -= 3f;
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
        Debug.Log("RSkill");
    }

    public override SkillHitBox HitBox => this.hitBox;
}
