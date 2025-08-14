using UnityEngine;

public class BossAttackState : BossBaseState
{
    public BossAttackState(BossEnemy bossEnemy) : base(bossEnemy) { }
    float animationDuration;
    float timer;

    public override void Enter()
    {
        Debug.Log("Attack Enter: " + bossEnemy.GetType().Name);

        timer = 0f;

        var thisEnemy = bossEnemy.GetType().Name;
        SoundManager.Instance.Play2DSound(thisEnemy + " Attack Sound");

        bossEnemy.BossAnimator.PlayRandomAttack(2);
        animationDuration = bossEnemy.BossAnimator.GetClipByName("Attack").length;

        bossEnemy.EnemyAI.ResetMove();
    }
    public override void Update() 
    {
        // Debug.Log($"[AttackState] isHit 상태: {enemy.isHit}");
        if(bossEnemy.isDead)
        {
            bossEnemy.ChangeState(new BossDeadState(bossEnemy));
            return;
        }

        if(bossEnemy.isHit) 
        {
            bossEnemy.ChangeState(new BossHitState(bossEnemy));
        }
        
        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            bossEnemy.ChangeState(new BossIdleState(bossEnemy));
        }
    }
    public override void Exit()
    {
        Debug.Log("Enemy Attack State Exit");
        bossEnemy.EnemyAI.isAttack = false;
    }
}
