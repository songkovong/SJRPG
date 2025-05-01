using UnityEngine;

public class GuardState : BaseState
{
    public GuardState(Player player) : base(player) { }

    float GuardMoveSpeed = 0.1f;

    public override void Enter()
    {
        Debug.Log("Enter Guard");
        player.StartOrbitTrail();
        player.playerStat.isGodmode = true;
    }

    public override void Update()
    {
        player.PlayerAnimator.PlayGuard(true);

        player.PlayerMove(GuardMoveSpeed);
        player.PlayerAnimator.SetMove(
            player.InputDirection.magnitude * GuardMoveSpeed * .75f, 
            player.localMovement.x, 
            player.localMovement.z
        );
        
        // player.OrbitRotation();

        if(!player.GuardPressed)
        {
            player.PlayerAnimator.PlayGuard(false);
            // player.ChangeState(
            //     player.InputDirection != Vector2.zero ? new MoveState(player) : new IdleState(player)
            // );
            player.ChangeState(new MoveState(player));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Guard");
        player.EndOrbitTrail();
        player.playerStat.isGodmode = false;
    }
}
