using UnityEngine;
public class DodgeState : BaseState
{
    public DodgeState(Player player) : base(player) { }

    float timer;

    public override void Enter()
    {
        timer = 0f;
        Debug.Log("Enter Dodge");
        player.PlayerAnimator.PlayDodge();
    }

    public override void Update()
    {
        var animationDuration = player.PlayerAnimator.GetClipByName("Dodge").length;
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
        Debug.Log("Exit Dodge"); 
    }
}
