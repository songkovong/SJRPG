using System.Runtime.Serialization;
using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    public EnemyHitState(Enemy enemy) : base(enemy) {}

    float animationDuration;
    float timer;

    public override void Enter()
    {
        Debug.Log("Enemy Hit State Enter");
        timer = 0f;

        enemy.EnemyAnimator.PlayHit();
        animationDuration = enemy.EnemyAnimator.GetClipByName("GetHit").length;

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
        Debug.Log("Enemy Hit State Enter");
        enemy.isHit = false;
    }
}
