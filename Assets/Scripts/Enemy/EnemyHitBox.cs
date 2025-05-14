using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStat>()?.TakeDamage(enemy.attackDamage);
        }
    }
}
