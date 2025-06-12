using UnityEngine;

public class SkillHitBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        other.GetComponent<IDamageable>()?.TakeDamage(player.playerStat.finalDamage);
    }

    public void HitboxOn() => this.gameObject.SetActive(true);
    public void HitboxOff() => this.gameObject.SetActive(false);
}
