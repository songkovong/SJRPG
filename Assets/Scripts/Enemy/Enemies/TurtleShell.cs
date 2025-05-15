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
        maxHealth = 1f;
        currentHealth = maxHealth;
        godmodeDuration = 1f;
    }

    public override void TakeDamage(float getDamage)
    {
        base.TakeDamage(getDamage);
        Debug.Log("TurtelShell Damaged");
    }
}
