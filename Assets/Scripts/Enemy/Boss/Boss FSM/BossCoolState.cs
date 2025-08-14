using UnityEngine;

public class BossCoolState : BossBaseState
{
    public BossCoolState(BossEnemy bossEnemy) : base(bossEnemy) { }

    float timer;
    float cooltime;

    public override void Enter()
    {
        timer = 0f;
        Debug.Log("Cool Enter: " + bossEnemy.GetType().Name);
        bossEnemy.BossAnimator.PlayCool(true);
        cooltime = bossEnemy.attackCooltime;
        Debug.Log("Attack Cooltime " + cooltime);
    }
    public override void Update()
    {
        bossEnemy.EnemyAI.RotateToPlayer();

        if(bossEnemy.isDead) 
        {
            bossEnemy.ChangeState(new BossDeadState(bossEnemy));
            return;
        }

        if(bossEnemy.isHit)
        {
            bossEnemy.ChangeState(new BossHitState(bossEnemy));
            return;
        }

        timer += Time.deltaTime;

        if(timer >= cooltime)
        {
            if(bossEnemy.EnemyAI.isAttack)
            {
                bossEnemy.ChangeState(new BossAttackState(bossEnemy));
                return;
            }
            bossEnemy.ChangeState(new BossIdleState(bossEnemy));
        }

    }
    public override void Exit()
    {
        Debug.Log("Enemy Cool State Enter");
        bossEnemy.BossAnimator.PlayCool(false);
    }
}
