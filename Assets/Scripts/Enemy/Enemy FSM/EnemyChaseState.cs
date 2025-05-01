using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(Enemy enemy) : base(enemy) {}

    public override void Enter()
    {
        Debug.Log("Enemy Chase State Enter");
    }
    public override void Update() 
    {
        enemy.EnemyAI.MoveToPlayer();

        if(enemy.isHit) enemy.ChangeState(new EnemyHitState(enemy));

        if(enemy.EnemyAI.isAttack) enemy.ChangeState(new EnemyAttackState(enemy));

        if(!enemy.EnemyAI.IsDetective) enemy.ChangeState(new EnemyIdleState(enemy));
    }
    public override void Exit()
    {
        Debug.Log("Enemy Chase State Enter");
    }
}
