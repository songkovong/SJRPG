using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyBaseState enemyCurrentState;

    EnemyAnimator enemyAnimator;
    Animator animator;
    EnemyAI enemyAI;

    public float attackCooltime { get; private set; } = 2f;

    public bool isHit { get; set; }
    void Start()
    {
        ChangeState(new EnemyIdleState(this));

        animator = GetComponent<Animator>();
        enemyAnimator = new EnemyAnimator(animator);
        enemyAI = GetComponent<EnemyAI>();
    }

    void Update()
    {
        enemyCurrentState?.Update();
        // HitValue(transform.position + (Vector3.up * 0.4f), 0.5f, "Player", 1f); // when attack
        enemyAI.DetectPlayer();
        enemyAI.DetectAttackPlayer();
    }

    public void ChangeState(EnemyBaseState newstate)
    {
        enemyCurrentState?.Exit();
        enemyCurrentState = newstate;
        enemyCurrentState.Enter();
    }

    void HitValue(Vector3 hitPosition, float hitRadius, string hitLayer, float hitDamage)
    {
        Collider[] hits = Physics.OverlapSphere(hitPosition, hitRadius, LayerMask.GetMask(hitLayer));
        foreach (Collider hit in hits)
        {
            hit.GetComponent<PlayerStat>()?.TakeDamage(hitDamage);
            // hit.GetComponent<Player>().isHit = true;
            // hit.GetComponent<Player>().hitDamage = hitDamage;
        }
    }

    public void TakeDamage(float getDamage)
    {
        isHit = true;
        Debug.Log("Damage + " + getDamage);
    }

    public EnemyAnimator EnemyAnimator => enemyAnimator;
    public Animator Animator => animator;
    public EnemyAI EnemyAI => enemyAI;
}
