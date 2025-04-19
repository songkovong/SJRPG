using UnityEngine;
public class MoveState : BaseState
{
    public MoveState(Player player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Enter Move");
    }

    public override void Update()
    {
        player.PlayerMove();
        player.PlayerRotation();
        player.PlayerAnimator.SetMove(player.SprintPressed ? 1f : 0.5f);

        if (player.InputDirection.magnitude == 0f)
        {
            player.ChangeState(new IdleState(player));
            return;
        }

        if (player.AttackPressed)
        {
            player.ChangeState(new AttackState(player));
            return;
        }

        if (player.DodgePressed)
        {
            player.ChangeState(new DodgeState(player));
            return;
        }

        if (player.GuardPressed)
        {
            player.ChangeState(new GuardState(player));
            return;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Move");
    }
}
