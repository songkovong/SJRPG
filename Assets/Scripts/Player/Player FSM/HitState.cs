using UnityEngine;

public class HitState : BaseState
{
    public HitState(Player player) : base(player) { }

    float animationDuration;
    float timer;

    public override void Enter()
    {
        timer = 0f;

        Debug.Log("Enter Hit");
        player.PlayerAnimator.PlayHit();
        animationDuration = player.PlayerAnimator.GetClipByName("Hit").length;

        // player.playerStat.finalDamage = player.playerStat.attackDamage + player.playerStat.weaponDamage;
    }

    public override void Update()
    {
        // player.playerStat.HitValue(
        //     player.attackHitbox.transform.position, 
        //     new Vector3(0.2f, 1.2f, 0.1f), 
        //     player.attackHitbox.transform.rotation, 
        //     "Enemy"
        // );
        // player.playerStat.HitValue();

        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            player.ChangeState(new MoveState(player));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Hit");
        player.isHit = false;
    }
}
