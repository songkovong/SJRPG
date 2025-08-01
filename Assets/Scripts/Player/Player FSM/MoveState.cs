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

        if (player.playerStat.data.isLevelUp)
        {
            player.ChangeState(new LevelUpState(player));
            return;
        }

        if (!player.SprintPressed)
        {
            if (player.AttackPressed && WeaponManager.Instance.currentWeaponItem != null)
            {
                player.ChangeState(new AttackState(player));
                return;
            }

            if (player.SpaceSkillPressed && player.playerStat.spaceSkill.CanActivateSkill())
            {
                player.playerStat.spaceSkill.UseSkill();
                player.ChangeState(new SkillState(player));
                return;
            }
            
            if (player.CSkillPressed && player.playerStat.cSkill.CanActivateSkill())
            {
                player.playerStat.cSkill.UseSkill();
                player.ChangeState(new SkillState(player));
                return;
            }

            if (player.RSkillPressed && player.playerStat.rSkill.CanActivateSkill())
            {
                player.playerStat.rSkill.UseSkill();
                player.ChangeState(new SkillState(player));
                return;
            }

            if (player.GuardPressed && player.playerStat.guardSkill.CanActivateSkill())
            {
                Debug.Log("Guard State Enter");
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
