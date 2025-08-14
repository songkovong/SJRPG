using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public PlayerStat playerstat;
    public EnemyBaseState enemyCurrentState;
    public EnemySpawner spawner;

    EnemyAnimator enemyAnimator;
    Animator animator;
    EnemyAI enemyAI;
    HitColor enemyHitColor;

    public int thisEnemyCode { get; protected set; } = -1;

    public float attackCooltime { get; protected set; } = 2f;
    public float detectRadius { get; protected set; } = 10f;
    public float detectAttackRadius { get; protected set; } = 1.5f;
    public float moveSpeed { get; protected set; } = 1.2f;
    public float rotationSpeed { get; protected set; } = 2000f;
    public float attackDamage { get; protected set; } = 1f;
    public float maxHealth { get; protected set; } = 100f;
    public float currentHealth { get; protected set;} = 100f;
    public float godmodeDuration { get; protected set; } = 0.5f;
    public float dependRate { get; protected set; } = 0.1f;

    public bool isGodmode { get; protected set; } = false;
    public bool isHit { get; set; } = false;
    public bool isDead { get; set; }

    public bool isBoss { get; protected set; } = false;

    public GameObject hitbox;
    public GameObject damageText;
    public Transform damagePos;
    


    protected virtual void Awake() { }

    void Start()
    {
        playerstat = GameObject.FindWithTag("Player")?.GetComponent<PlayerStat>();
        animator = GetComponent<Animator>();
        enemyAnimator = new EnemyAnimator(animator);
        enemyAI = GetComponent<EnemyAI>();

        enemyHitColor = GetComponent<HitColor>();

        hitbox = GameObject.FindWithTag("Enemy Hitbox");
        HitboxOff();

        currentHealth = maxHealth;

        ChangeState(new EnemyIdleState(this));
    }

    void Update()
    {
        enemyCurrentState?.Update();

        if (enemyAI != null)
        {
            enemyAI.DetectPlayer(detectRadius);
            enemyAI.DetectAttackPlayer(detectAttackRadius);
        }
    }

    void OnEnable() // If object Enable
    {
        if (isDead)
        {
            isDead = false;
            isHit = false;
            currentHealth = maxHealth;
            ChangeState(new EnemyIdleState(this));
        }
    }

    public void ChangeState(EnemyBaseState newstate)
    {
        enemyCurrentState?.Exit();
        enemyCurrentState = newstate;
        enemyCurrentState.Enter();
    }

    public virtual void TakeDamage(int minDamage, int maxDamage)
    {
        // GodMode
        if (isGodmode) return; 
        if (isDead) return;

        var thisEnemy = this.GetType().Name;
        SoundManager.Instance.Play2DSound(thisEnemy + " Hit Sound");

        var getDamage = Random.Range(minDamage, maxDamage);

        getDamage = (int)(getDamage * (1 - Random.Range(dependRate / 2, dependRate)));

        if (getDamage == 0)
        {
            if (damagePos != null && damageText != null)
            {
                GameObject missText = Instantiate(damageText, damagePos.position, Quaternion.identity);
                missText.GetComponent<DamageText>().MissText();
            }
            StartCoroutine(GodmodeCoroutine());
            return;
        }

        isHit = true;

        currentHealth -= getDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Damage Text
        // GameObject dmgtext = Instantiate(damageText);
        // dmgtext.transform.position = damagePos.position;
        // dmgtext.GetComponent<DamageText>().damage = getDamage;
        if (damagePos != null && damageText != null)
        {
            var dmgText = Instantiate(damageText, damagePos.position, Quaternion.identity);
            dmgText.GetComponent<DamageText>().DmgText(getDamage);
        }

        if(playerstat != null)
        {
            if (playerstat.cSkill.isDuration)
            {
                playerstat.Heal(getDamage * 0.05f);
            }
        }
        
        if (currentHealth <= 0)
        {
            isDead = true;
        }
        else
        {
            StartCoroutine(GodmodeCoroutine());
        }

        StartCoroutine(HitColorCoroutine());

        Debug.Log("Enemy Current Health = " + currentHealth);
    }

    public virtual void EnemyDie() // In DeadState
    {
        if (spawner != null)
        {
            spawner.NotifyEnemyDead(gameObject);
        }
        else
        {
            DestroyEnemy();
        }
    }

    public virtual void DropItem() { }
    public virtual void DropCoin() { }

    public virtual void ExpUp()
    {
        playerstat.data.expCount += 1;
    }

    private IEnumerator GodmodeCoroutine()
    {
        Debug.Log("Godmode");
        isGodmode = true;

        yield return new WaitForSeconds(godmodeDuration);

        isGodmode = false;
    }

    public IEnumerator HitColorCoroutine()
    {
        Debug.Log("ColorChange");
        enemyHitColor.ChangeColor(enemyHitColor.renderers, Color.grey);

        yield return new WaitForSeconds(godmodeDuration);

        enemyHitColor.ReChangeColor(enemyHitColor.renderers, enemyHitColor.originalColors);
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
