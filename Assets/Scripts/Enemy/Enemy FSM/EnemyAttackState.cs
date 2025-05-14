using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(Enemy enemy) : base(enemy) {}

    float animationDuration;
    float timer;

    public override void Enter()
    {
        Debug.Log("Attack Enter: " + enemy.GetType().Name);

        timer = 0f;

        enemy.EnemyAnimator.PlayAttack();
        animationDuration = enemy.EnemyAnimator.GetClipByName("Attack").length;

        enemy.EnemyAI.ResetMove();
    }
    public override void Update() 
    {
        Debug.Log($"[AttackState] isHit 상태: {enemy.isHit}");
        if(enemy.isDead)
        {
            enemy.ChangeState(new EnemyDeadState(enemy));
            return;
        }

        if(enemy.isHit) 
        {
            enemy.ChangeState(new EnemyHitState(enemy));
        }
        
        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }
    public override void Exit()
    {
        Debug.Log("Enemy Attack State Exit");
        enemy.EnemyAI.isAttack = false;
    }
}
