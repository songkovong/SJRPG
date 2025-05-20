using UnityEngine;

public class LevelUpState : BaseState
{
    public LevelUpState(Player player) : base(player) {}

    float animationDuration;
    float timer;

    public override void Enter()
    {
        player.playerStat.isGodmode = true;

        timer = 0f;

        Debug.Log("Enter LvUp");
        player.PlayerAnimator.PlayLevelUp();
        player.playerAnimator.SetMove(0, 0, 0);
        animationDuration = player.PlayerAnimator.GetClipByName("LevelUp").length;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            player.ChangeState(new MoveState(player));
        }
    }

    public override void Exit()
    {
        player.playerStat.isGodmode = false;
        Debug.Log("Exit Lvup");
        player.playerStat.isLevelUp = false;
    }
}
