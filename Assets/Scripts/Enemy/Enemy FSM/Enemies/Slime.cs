using UnityEngine;

public class Slime : Enemy, IDamageable
{
    public override void TakeDamage(float getDamage)
    {
        Debug.Log("Slime Damage");
        base.TakeDamage(getDamage);
    }
}
