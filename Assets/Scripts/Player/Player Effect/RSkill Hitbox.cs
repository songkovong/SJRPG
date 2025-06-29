using UnityEngine;

public class RSkillHitbox : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject || other.CompareTag("Player"))
            return;

        other.GetComponent<IDamageable>()?.TakeDamage(player.playerStat.data.finalDamage);
    }
}
