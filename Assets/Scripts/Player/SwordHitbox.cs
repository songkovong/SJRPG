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

        other.GetComponent<Enemy>()?.TakeDamage(player.playerStat.finalDamage);
    }
}
