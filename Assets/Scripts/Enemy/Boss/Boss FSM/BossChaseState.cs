using UnityEngine;

public class BossChaseState : BossBaseState
{
    public BossChaseState(BossEnemy bossEnemy) : base(bossEnemy) { }

    public override void Enter()
    {
        Debug.Log("Chase Enter: " + bossEnemy.GetType().Name);
        bossEnemy.BossAnimator.PlayChase(true);
    }
    public override void Update() 
    {
        bossEnemy.EnemyAI.MoveToPlayer(bossEnemy.moveSpeed, bossEnemy.rotationSpeed);
        // Debug.Log($"[ChaseState] isHit 상태: {enemy.isHit}");

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

        if(!bossEnemy.EnemyAI.IsDetective) {
            bossEnemy.ChangeState(new BossIdleState(bossEnemy));
            return;
        }
    }
    public override void Exit()
    {
        Debug.Log("Enemy Chase State Enter");
        bossEnemy.BossAnimator.PlayChase(false);
    }
}
