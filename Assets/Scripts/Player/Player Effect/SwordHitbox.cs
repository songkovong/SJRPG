using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
        HitboxOff();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject || other.CompareTag("Player"))
            return;

        other.GetComponent<IDamageable>()?.TakeDamage(player.playerStat.data.finalDamage);
    }

    public void HitboxOn() => this.gameObject.SetActive(true);
    public void HitboxOff() => this.gameObject.SetActive(false);
}
