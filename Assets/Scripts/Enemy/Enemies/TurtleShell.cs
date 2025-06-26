using UnityEngine;

public class TurtleShell : Enemy, IDamageable
{
    protected override void Awake()
    {
        base.Awake();
        attackCooltime = 1.5f;
        detectRadius = 7f;
        detectAttackRadius = 2.5f;
        moveSpeed = 2f;
        rotationSpeed = 2000f;
        attackDamage = 3f;
        maxHealth = 20f;
        currentHealth = maxHealth;
        godmodeDuration = 1f;
        dependRate = 0.5f;
    }

    public override void TakeDamage(float getDamage)
    {
        base.TakeDamage(getDamage);
        Debug.Log("TurtelShell Damaged");
    }

    public override void ExpUp()
    {
        base.ExpUp();
        playerstat.data.expCount += 20;
    }

    public override void EnemyDie() // In DeadState
    {
        if (spawner != null)
        {
            spawner.NotifyEnemyDead(gameObject);
        }
        else
        {
            DestroyEnemy();
        }

        // Item Drop
        Debug.Log("Item Dropped");
    }
}
