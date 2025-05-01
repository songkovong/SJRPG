using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(Enemy enemy) : base(enemy) {}

    public override void Enter()
    {
        Debug.Log("Enemy Idle State Enter");
    }
    public override void Update() 
    {
        if(enemy.isHit) enemy.ChangeState(new EnemyHitState(enemy));

        if(enemy.EnemyAI.IsDetective) enemy.ChangeState(new EnemyChaseState(enemy));

        if(enemy.EnemyAI.isAttack) enemy.ChangeState(new EnemyAttackState(enemy));
    }
    public override void Exit()
    {
        Debug.Log("Enemy Idle State Exit");
    }
}
