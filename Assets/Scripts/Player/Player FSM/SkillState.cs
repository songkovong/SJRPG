using UnityEngine;

public class SkillState : BaseState
{
    public SkillState(Player player) : base(player) { }

    float animationDuration;
    float timer;
    float skillMoveSpeed = 1f;

    public override void Enter()
    {
        timer = 0f;
        Debug.Log("Enter Skill");
        player.PlayerAnimator.PlaySkill();
        animationDuration = player.PlayerAnimator.GetClipByName("Skill").length;
        player.isSkill = true;
        player.StartTrail();
    }

    public override void Update()
    {
        player.PlayerMove(skillMoveSpeed);
        // player.PlayerRotation();
        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            // player.ChangeState(
            //     player.InputDirection != Vector2.zero ? new MoveState(player) : new IdleState(player)
            // );
            player.ChangeState(new MoveState(player));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Skill");
        player.isSkill = false;
        player.EndTrail();
    }
}
