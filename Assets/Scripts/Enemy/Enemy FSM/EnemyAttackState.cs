using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(Enemy enemy) : base(enemy) {}

    float animationDuration;
    float timer;

    public override void Enter()
    {
        Debug.Log("Attack Enter: " + enemy.GetType().Name);
        var thisEnemy = enemy.GetType().Name;

        if (enemy.isBoss)
        {
            var rand = Random.Range(1, 3);
            enemy.EnemyAnimator.SetAttackIndex(rand);
            animationDuration = enemy.EnemyAnimator.GetClipByName("Attack " + rand.ToString()).length;
        }
        else
        {
            animationDuration = enemy.EnemyAnimator.GetClipByName("Attack").length;
            SoundManager.Instance.Play2DSound(thisEnemy + " Attack Sound");
        }

        timer = 0f;

        enemy.EnemyAnimator.PlayAttack();

        enemy.EnemyAI.ResetMove();
    }
    public override void Update() 
    {
        // Debug.Log($"[AttackState] isHit 상태: {enemy.isHit}");
        if(enemy.isDead)
        {
            enemy.ChangeState(new EnemyDeadState(enemy));
            return;
        }

        if(enemy.isHit) 
        {
            if (!enemy.isBoss)
            {
                enemy.ChangeState(new EnemyHitState(enemy));
            }
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
