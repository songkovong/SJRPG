using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyCoolState : EnemyBaseState
{
    public EnemyCoolState(Enemy enemy) : base(enemy) {}

    float timer;
    float cooltime = 1.5f;

    public override void Enter()
    {
        timer = 0f;
        Debug.Log("Cool Enter: " + enemy.GetType().Name);
        enemy.EnemyAnimator.PlayCool(true);
    }
    public override void Update()
    {
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
