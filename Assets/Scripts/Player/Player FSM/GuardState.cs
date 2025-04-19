using UnityEngine;

public class GuardState : BaseState
{
    public GuardState(Player player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Enter Guard");
    }

    public override void Update()
    {
        player.PlayerAnimator.PlayGuard(true);

        if(!player.GuardPressed)
        {
            player.PlayerAnimator.PlayGuard(false);
            player.ChangeState(
                player.InputDirection != Vector2.zero ? new MoveState(player) : new IdleState(player)
            );
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Guard");
    }
}
