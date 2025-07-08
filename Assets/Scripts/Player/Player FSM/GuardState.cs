using UnityEngine;

public class GuardState : BaseState
{
    public GuardState(Player player) : base(player) { }

    float GuardMoveSpeed = 0.1f;
    float exitMana = 0.1f;

    public override void Enter()
    {
        Debug.Log("Enter Guard");
        if (!player.playerStat.guardSkill.CanActivateSkill())
        {
            player.ChangeState(new MoveState(player));
        }

        player.GuardTrail.StartTrail();
        player.playerStat.data.isGodmode = true;
    }

    public override void Update()
    {
        if (player.playerStat.data.currentMagic < exitMana)
        {
            ExitGuard();
        }

        player.playerStat.data.currentMagic -= player.playerStat.guardSkill.masteryStat * Time.deltaTime;

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
        player.PlayerAnimator.PlayGuard(false);
        player.ChangeState(new MoveState(player));
    }
}
