using UnityEngine;

public class SkillState : BaseState
{
    public SkillState(Player player) : base(player) { }

    float timer;

    public override void Enter()
    {
        timer = 0f;
        Debug.Log("Enter Skill");
        player.PlayerAnimator.PlaySkill();
    }

    public override void Update()
    {
        var animationDuration = player.PlayerAnimator.GetClipByName("Skill").length;
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
    }
}
