using UnityEngine;

public class Slime : Enemy, IDamageable
{
    protected override void Awake()
    {
        base.Awake();
        attackCooltime = 1f;
        detectRadius = 7f;
        detectAttackRadius = 1.5f;
        moveSpeed = 1f;
        rotationSpeed = 20000f;
        attackDamage = 2f;
        maxHealth = 10f;
        currentHealth = maxHealth;
        godmodeDuration = 1f;
    }

    public override void TakeDamage(float getDamage)
    {
        base.TakeDamage(getDamage);
        Debug.Log("Slime Damaged");
    }
}
