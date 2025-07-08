using UnityEngine;

public class GuardState : BaseState
{
    public GuardState(Player player) : base(player) { }

    float GuardMoveSpeed = 0.1f;
    float exitMana = 0.1f;

    float manaTickInterval = 1f;
    float manaTickTimer = 0f;

    bool isExit = false;

    public override void Enter()
    {
        Debug.Log("Enter Guard");
        isExit = false;

        if (!player.playerStat.guardSkill.CanActivateSkill())
        {
            player.ChangeState(new MoveState(player));
            return;
        }

        player.GuardTrail.StartTrail();
        player.playerStat.data.isGodmode = true;

        manaTickTimer = manaTickInterval;
    }

    public override void Update()
    {
        if (isExit)
        {
            return;
        }

        manaTickTimer -= Time.deltaTime;

        if (manaTickTimer <= 0f)
        {
            manaTickTimer = manaTickInterval;

            player.playerStat.data.currentMagic -= player.playerStat.guardSkill.masteryStat;

            if (player.playerStat.data.currentMagic < player.playerStat.guardSkill.masteryStat + exitMana)
            {
                ExitGuard();
                return;
            }
        }

        player.PlayerAnimator.PlayGuard(true);

        player.PlayerMove(GuardMoveSpeed);
        player.PlayerAnimator.SetMove(
            player.InputDirection.magnitude * GuardMoveSpeed * .75f,
            player.localMovement.x,
            player.localMovement.z
        );

        if (!player.GuardPressed)
        {
            ExitGuard();
        }

    }

    public override void Exit()
    {
        Debug.Log("Exit Guard");

        player.GuardTrail.EndTrail();
        player.playerStat.data.isGodmode = false;
    }

    void ExitGuard()
    {
        if (isExit) return;
        isExit = true;

        player.PlayerAnimator.PlayGuard(false);
        player.ChangeState(new MoveState(player));
    }
}
