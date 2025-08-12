using UnityEngine;

public class BossIdleState : BossBaseState
{
    public BossIdleState(BossEnemy bossEnemy) : base(bossEnemy) { }

    public override void Enter()
    {
        Debug.Log("Idle Enter: " + bossEnemy.GetType().Name);
    }
    public override void Update()
    {
        if(bossEnemy.isDead)
        {
            bossEnemy.ChangeState(new BossDeadState(bossEnemy));
            return;
        }

        if(bossEnemy.isHit) {
            bossEnemy.ChangeState(new BossHitState(bossEnemy)); 
            return;
        }

        if(bossEnemy.EnemyAI.isAttack) {
            bossEnemy.ChangeState(new BossCoolState(bossEnemy)); 
            return;
        }

        if(bossEnemy.EnemyAI.IsDetective) {
            bossEnemy.ChangeState(new BossChaseState(bossEnemy)); 
            return;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Boss Idle State");
    }

}
