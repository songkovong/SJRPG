using UnityEngine;

public class SkillState : BaseState
{
    public SkillState(Player player) : base(player) { }

    float animationDuration;
    float timer;
    float skillMoveSpeed = 1f;

    public override void Enter()
    {
        player.playerStat.finalDamage = player.playerStat.SkillDmg();

        Debug.Log("Player stat skill code = " + player.playerStat.SkillCode);

        var skillCodeHash = player.playerStat.SkillCode.ToString();
        player.playerStat.canSkill = false;
        player.playerStat.skillCooltimeTimer -= player.playerStat.skillCooltime;
        player.playerStat.currentMagic -= player.playerStat.SkillMagic;

        // Player Skill is empty
        // if(skillNameHash == "" || skillNameHash == null) player.ChangeState(new MoveState(player));

        timer = 0f;
        Debug.Log("Enter Skill");
        player.PlayerAnimator.PlaySkill(skillCodeHash);
        animationDuration = player.PlayerAnimator.GetClipByName("Skill " + skillCodeHash).length;

        player.isSkill = true;
        player.playerStat.isGodmode = true;

        player.AttackTrail.StartTrail();
        player.AttackHitbox.HitboxOn();
    }

    public override void Update()
    {
        player.PlayerMove(skillMoveSpeed);
        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            player.ChangeState(new MoveState(player));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Skill");

        player.isSkill = false;
        player.playerStat.isGodmode = false;

        player.AttackTrail.EndTrail();
        player.AttackHitbox.HitboxOff();
    }
}
