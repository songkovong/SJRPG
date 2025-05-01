using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(Enemy enemy) : base(enemy) {}

    float animationDuration;
    float timer;

    public override void Enter()
    {
        Debug.Log("Enemy Attack State Enter");

        timer = 0f;

        enemy.EnemyAnimator.PlayAttack();
        animationDuration = enemy.EnemyAnimator.GetClipByName("Attack").length;

        enemy.EnemyAI.ResetMove();
    }
    public override void Update() 
    {
        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }
    public override void Exit()
    {
        Debug.Log("Enemy Attack State Enter");
        enemy.EnemyAI.isAttack = false;
    }
}
