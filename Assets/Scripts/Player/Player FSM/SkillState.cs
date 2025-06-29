using System.Collections;
using UnityEngine;

public class SkillState : BaseState
{
    public SkillState(Player player) : base(player) { }

    float animationDuration;
    float timer;
    float skillMoveSpeed = 1f;

    float currentCode;

    public override void Enter()
    {
        player.isSkill = true;
        player.playerStat.data.isGodmode = true;

        currentCode = player.skillCode;

        player.playerStat.data.finalDamage = player.playerStat.SkillDmg(player.skillDamage);

        Debug.Log("Skill Damage = " + player.skillDamage);

        var skillCodeHash = currentCode.ToString();

        timer = 0f;
        Debug.Log("Enter Skill " + skillCodeHash);
        player.PlayerAnimator.PlaySkill(skillCodeHash);

        animationDuration = player.PlayerAnimator.GetClipByName("Skill " + skillCodeHash).length;

        if (currentCode == 1)
        {
            player.AttackTrail.StartTrail();
            player.playerStat.spaceSkill.HitBox.HitboxOn();
        }
        else if (currentCode == 2)
        {
            player.playerStat.cSkill.HitBox.HitboxOn();
        }
        else if (currentCode == 3)
        {
            player.AttackTrail.StartTrail();
            animationDuration += .3f;
        }
    }

    public override void Update()
    {
        player.PlayerAnimator.SetMove(0, 0, 0);

        if (currentCode == 1)
        {
            player.PlayerMove(skillMoveSpeed);
        }

        timer += Time.deltaTime;

        if (timer >= animationDuration)
        {
            player.ChangeState(new MoveState(player));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Skill");

        player.isSkill = false;
        player.playerStat.data.isGodmode = false;

        player.AttackTrail.EndTrail();
        player.playerStat.spaceSkill.HitBox.HitboxOff();
        player.playerStat.cSkill.HitBox.HitboxOff();
    }
}
