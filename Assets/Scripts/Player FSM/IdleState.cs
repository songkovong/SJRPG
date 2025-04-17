using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Player player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Enter Idle");
    }

    public override void Update()
    {
        if (player.InputDirection.magnitude > 0f)
        {
            player.ChangeState(new MoveState(player));
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
    }

    public override void Exit()
    {
        Debug.Log("Exit Idle");
    }
}
