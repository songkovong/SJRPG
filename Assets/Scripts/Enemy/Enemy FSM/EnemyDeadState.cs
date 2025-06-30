using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(Enemy enemy) : base(enemy) {}

    float animationDuration;
    float timer;

    public override void Enter()
    {
        enemy.DropItem();
        enemy.DropCoin();
        enemy.EnemyAI.ResetMove();
        enemy.EnemyAnimator.PlayDead(true);
        animationDuration = enemy.EnemyAnimator.GetClipByName("Dead").length + 0.5f;
        enemy.ExpUp();
    }
    public override void Update()
    {
        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            enemy.EnemyDie();
        }
    }
    public override void Exit()
    {

    }
}
