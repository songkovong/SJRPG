using UnityEngine;

public class Slime : EnemyBase
{
    EnemyAI enemyAI;
    Animator animator;
    EnemyAnimator enemyAnimator;


    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
        enemyAnimator = new EnemyAnimator(animator);
        enemyAI = GetComponent<EnemyAI>();

        ChangeState(ENEMY_STATE.Idle);
    }

    private void Update() 
    {
        //ChangeState(ENEMY_STATE.Chase);

        HandleState();
    }

    protected override void OnStateChanged(ENEMY_STATE newState)
    {
        base.OnStateChanged(newState);

        switch(newState)
        {
            case ENEMY_STATE.Idle:
                break;
            case ENEMY_STATE.Chase:
                enemyAnimator.PlayChase(enemyAI.isDetective);
                enemyAI.MoveToPlayer();
                break;
            case ENEMY_STATE.Cool:
                break;
            case ENEMY_STATE.Attack:
                break;
            case ENEMY_STATE.Hit:
                break;
            case ENEMY_STATE.Dead:
                break;
        }
    }

    private void HandleState()
    {
        if(enemyAI.isDetective)
        {
            ChangeState(ENEMY_STATE.Chase);
        }

        else if(enemyAI.isAttack)
        {
            ChangeState(ENEMY_STATE.Attack);
        }

        else 
        {
            enemyAI.isDetective = false;
            ChangeState(ENEMY_STATE.Idle);
        }
    }
}
