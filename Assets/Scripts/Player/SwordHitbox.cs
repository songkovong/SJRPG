using Unity.VisualScripting;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject || other.CompareTag("Player"))
        return;

        // if(other.CompareTag("Enemy"))
        // {
        //     Enemy enemy = other.GetComponent<Enemy>();
        //     enemy.TakeDamage(player.playerStat.finalDamage);
        // }

        other.GetComponent<IDamageable>()?.TakeDamage(player.playerStat.finalDamage);
    }
}
