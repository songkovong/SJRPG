using System.Collections;
using Mono.Cecil.Cil;
using UnityEngine;

public class SpaceSkill : Skill
{
    protected override void Awake()
    {
        base.Awake();
        code = 1;
        level = 1;
        cooltime = 10f;
        damage = 2f;
        cost = 10f;
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
            cooltime -= 1f;
            damage++;
            cost -= 1;
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
        Debug.Log("SpaceSkill");
    }

    public override SkillHitBox HitBox => this.hitBox;
}
