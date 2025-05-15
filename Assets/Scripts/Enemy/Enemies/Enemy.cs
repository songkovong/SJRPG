using System;
using System.Collections;
using Unity.Profiling;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public EnemyBaseState enemyCurrentState;
    public EnemySpawner spawner;

    EnemyAnimator enemyAnimator;
    Animator animator;
    EnemyAI enemyAI;

    public float attackCooltime { get; protected set; } = 2f;
    public float detectRadius { get; protected set; } = 10f;
    public float detectAttackRadius { get; protected set; } = 1.5f;
    public float moveSpeed { get; protected set; } = 1.2f;
    public float rotationSpeed { get; protected set; } = 2000f;
    public float attackDamage { get; protected set; } = 1f;
    public float maxHealth { get; protected set; } = 100f;
    public float currentHealth { get; protected set;} = 100f;
    public float godmodeDuration { get; protected set; } = 1f;

    public bool isGodmode { get; protected set; } = false;
    public bool isHit { get; set; } = false;
    public bool isDead { get; set; }

    public GameObject hitbox;


    protected virtual void Awake() { }

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyAnimator = new EnemyAnimator(animator);
        enemyAI = GetComponent<EnemyAI>();

        hitbox = GameObject.FindWithTag("Enemy Hitbox");
        HitboxOff();

        currentHealth = maxHealth;

        ChangeState(new EnemyIdleState(this));
    }

    void Update()
    {
        enemyCurrentState?.Update();
        enemyAI.DetectPlayer(detectRadius);
        enemyAI.DetectAttackPlayer(detectAttackRadius);
    }

    void OnEnable() // If object inable
    {
        isDead = false;
        isHit = false;
        currentHealth = maxHealth;
        ChangeState(new EnemyIdleState(this));
    }

    public void ChangeState(EnemyBaseState newstate)
    {
        enemyCurrentState?.Exit();
        enemyCurrentState = newstate;
        enemyCurrentState.Enter();
    }

    public virtual void TakeDamage(float getDamage)
    {
        if(isGodmode) return;
        if(isDead) return;

        isHit = true;

        currentHealth -= getDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(currentHealth <= 0)
        {
            isDead = true;
        }
        else
        {
            StartCoroutine(GodmodeCoroutine());
        }

        Debug.Log("Enemy Current Health = " + currentHealth);
    }

    public void EnemyDie() // In DeadState
    {
        if(spawner != null)
        {
            spawner.NotifyEnemyDead(gameObject);
        }
        else
        {
            DestroyEnemy();
        }
    }

    private IEnumerator GodmodeCoroutine()
    {
        Debug.Log("Godmode");
        isGodmode = true;

        yield return new WaitForSeconds(godmodeDuration);

        isGodmode = false;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public EnemyAnimator EnemyAnimator => enemyAnimator;
    public Animator Animator => animator;
    public EnemyAI EnemyAI => enemyAI;
    public void HitboxOn() => hitbox.SetActive(true);
    public void HitboxOff() => hitbox.SetActive(false);
}
