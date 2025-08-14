using UnityEngine;

public class BossDeadState : BossBaseState
{
    public BossDeadState(BossEnemy bossEnemy) : base(bossEnemy) { }

    float animationDuration;
    float timer;

    public override void Enter()
    {
        bossEnemy.DropItem();
        bossEnemy.DropCoin();
        bossEnemy.EnemyAI.ResetMove();
        bossEnemy.BossAnimator.PlayDead(true);
        animationDuration = bossEnemy.BossAnimator.GetClipByName("Dead").length + 0.5f;
        bossEnemy.ExpUp();
    }
    public override void Update()
    {
        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            bossEnemy.EnemyDie();
        }
    }
    public override void Exit()
    {

    }
}
