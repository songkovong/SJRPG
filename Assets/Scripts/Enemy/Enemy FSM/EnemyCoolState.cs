using UnityEngine;

public class EnemyCoolState : EnemyBaseState
{
    public EnemyCoolState(Enemy enemy) : base(enemy) {}

    float timer;
    float cooltime;

    public override void Enter()
    {
        timer = 0f;
        Debug.Log("Cool Enter: " + enemy.GetType().Name);
        enemy.EnemyAnimator.PlayCool(true);
        cooltime = enemy.attackCooltime;
        Debug.Log("Attack Cooltime " + cooltime);
    }
    public override void Update()
    {
        enemy.EnemyAI.RotateToPlayer();

        if(enemy.isDead) 
        {
            enemy.ChangeState(new EnemyDeadState(enemy));
            return;
        }

        if(enemy.isHit)
        {
            enemy.ChangeState(new EnemyHitState(enemy));
            return;
        }

        timer += Time.deltaTime;

        if(timer >= cooltime)
        {
            if(enemy.EnemyAI.isAttack)
            {
                enemy.ChangeState(new EnemyAttackState(enemy));
                return;
            }
            enemy.ChangeState(new EnemyIdleState(enemy));
        }

    }
    public override void Exit()
    {
        Debug.Log("Enemy Cool State Enter");
        enemy.EnemyAnimator.PlayCool(false);
    }
}
