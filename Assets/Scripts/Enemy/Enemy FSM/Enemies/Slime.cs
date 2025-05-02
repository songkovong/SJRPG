using UnityEngine;

public class Slime : Enemy, IDamageable
{
    public override void TakeDamage(float getDamage)
    {
        base.TakeDamage(getDamage);
        Debug.Log("Slime Damage");
    }
}
