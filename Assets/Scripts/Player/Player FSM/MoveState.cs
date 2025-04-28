using UnityEngine;
public class MoveState : BaseState
{
    public MoveState(Player player) : base(player) { }

    float defaultMoveSpeed = 1f;

    public override void Enter()
    {
        Debug.Log("Enter Move");
    }

    public override void Update()
    {
        player.PlayerMove(defaultMoveSpeed);
        // player.PlayerRotation();
        if(player.SprintPressed) {
            player.PlayerRotation();
        } else {
            player.PlayerMouseRotation();
        }
        // player.PlayerAnimator.SetMove(player.SprintPressed ? 1f : 0.5f);
        player.PlayerAnimator.SetMove(player.InputDirection.magnitude * (player.SprintPressed ? 1f : 0.5f), player.localMovement.x, player.localMovement.z);

        // if (player.InputDirection.magnitude == 0f)
        // {
        //     player.ChangeState(new IdleState(player));
        //     return;
        // }

        if (player.AttackPressed)
        {
            if(!player.SprintPressed)
            {
                player.ChangeState(new AttackState(player));
                return;
            }
            return;
        }

        if (player.SkillPressed)
        {
            if(!player.SprintPressed)
            {
                player.ChangeState(new SkillState(player));
            return;
            }
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
