using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(Enemy enemy) : base(enemy) {}

    public override void Enter()
    {
        Debug.Log("Chase Enter: " + enemy.GetType().Name);
        enemy.EnemyAnimator.PlayChase(true);
    }
    public override void Update() 
    {
        enemy.EnemyAI.MoveToPlayer();
        Debug.Log($"[ChaseState] isHit 상태: {enemy.isHit}");

        if(enemy.isHit) {
            enemy.ChangeState(new EnemyHitState(enemy)); 
            return;
        }

        if(enemy.EnemyAI.isAttack) {
            enemy.ChangeState(new EnemyCoolState(enemy)); 
            return;
        }

        if(!enemy.EnemyAI.IsDetective) {
            enemy.ChangeState(new EnemyIdleState(enemy)); 
            return;
        }
    }
    public override void Exit()
    {
        Debug.Log("Enemy Chase State Enter");
        enemy.EnemyAnimator.PlayChase(false);
    }
}
