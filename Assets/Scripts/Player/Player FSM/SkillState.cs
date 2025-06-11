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
        player.playerStat.isGodmode = true;

        currentCode = player.skillCode;

        player.playerStat.finalDamage = player.playerStat.SkillDmg(player.skillDamage);

        var skillCodeHash = currentCode.ToString();

        timer = 0f;
        Debug.Log("Enter Skill " + skillCodeHash);
        player.PlayerAnimator.PlaySkill(skillCodeHash);
        animationDuration = player.PlayerAnimator.GetClipByName("Skill " + skillCodeHash).length;

        player.AttackTrail.StartTrail();
        player.AttackHitbox.HitboxOn();
    }

    public override void Update()
    {
        if (currentCode == 1)
        {
            player.PlayerMove(skillMoveSpeed);
        }
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
