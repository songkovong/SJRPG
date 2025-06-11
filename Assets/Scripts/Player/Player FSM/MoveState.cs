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
        if(player.SprintPressed) {
            player.PlayerRotation();
        } else {
            player.PlayerMouseRotation();
        }
        player.PlayerAnimator.SetMove(
            player.InputDirection.magnitude * (player.SprintPressed ? 1f : 0.5f) * defaultMoveSpeed, 
            player.localMovement.x, 
            player.localMovement.z
        );

        if (player.playerStat.isLevelUp)
        {
            player.ChangeState(new LevelUpState(player));
            return;
        }

        if (!player.SprintPressed)
            {
                if (player.AttackPressed)
                {
                    player.ChangeState(new AttackState(player));
                    return;
                }

                if (player.SkillPressed && player.playerStat.canSkill && player.playerStat.canMagic)
                {
                    player.ChangeState(new SkillState(player));
                    return;
                }

                if (player.GuardPressed)
                {
                    player.ChangeState(new GuardState(player));
                    return;
                }
            }

        if(player.isHit)
        {
            player.ChangeState(new HitState(player));
            return;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Move");
    }
}
