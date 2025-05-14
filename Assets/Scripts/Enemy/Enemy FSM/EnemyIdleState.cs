using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(Enemy enemy) : base(enemy) {}

    public override void Enter()
    {
        Debug.Log("Idle Enter: " + enemy.GetType().Name);
    }
    public override void Update() 
    {
        Debug.Log($"[IdleState] isHit 상태: {enemy.isHit}");

        if(enemy.isDead)
        {
            enemy.ChangeState(new EnemyDeadState(enemy));
            return;
        }

        if(enemy.isHit) {
            enemy.ChangeState(new EnemyHitState(enemy)); 
            return;
        }

        if(enemy.EnemyAI.isAttack) {
            enemy.ChangeState(new EnemyCoolState(enemy)); 
            return;
        }

        if(enemy.EnemyAI.IsDetective) {
            enemy.ChangeState(new EnemyChaseState(enemy)); 
            return;
        }
    }
    public override void Exit()
    {
        Debug.Log("Enemy Idle State Exit");
    }
}
