using UnityEngine;

public class BossHitState : BossBaseState
{
    public BossHitState(BossEnemy bossEnemy) : base(bossEnemy) { }

    float animationDuration;
    float timer;

    public override void Enter()
    {
        Debug.Log("Hit Enter: " + bossEnemy.GetType().Name);
        timer = 0f;

        bossEnemy.EnemyAnimator.PlayHit();
        animationDuration = bossEnemy.EnemyAnimator.GetClipByName("GetHit").length;

        bossEnemy.EnemyAI.ResetMove();
    }

    public override void Update()
    {
        if(bossEnemy.isDead) 
        {
            bossEnemy.ChangeState(new BossDeadState(bossEnemy));
            return;
        }
        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            bossEnemy.ChangeState(new BossIdleState(bossEnemy));
        }
    }

    public override void Exit()
    {
        Debug.Log("Enemy Hit State Exit");
        bossEnemy.isHit = false;
    }

}
