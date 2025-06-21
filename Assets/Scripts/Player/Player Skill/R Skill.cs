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
        level = 1;
        cooltime = 30f; //30
        damage = 3f;
        cost = 30f; //30
        duration = 0f;
        maxLevel = 5;
    }

    protected override void Start()
    {
        base.Start();
        effect.SetActive(false);
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
