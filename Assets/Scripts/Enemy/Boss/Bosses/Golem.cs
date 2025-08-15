using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy, IDamageable
{
    public GameObject hitboxL;
    public GameObject hitboxR;
    [SerializeField] private List<GameObject> dropItems;
    [SerializeField] private GameObject dropCoin;
    protected override void Awake()
    {
        base.Awake();
        thisEnemyCode = 101;
        attackCooltime = 3f;
        detectRadius = 15f;
        detectAttackRadius = 3f;
        moveSpeed = 1f;
        rotationSpeed = 2000f;
        attackDamage = 30f;
        maxHealth = 200f;
        currentHealth = maxHealth;
        godmodeDuration = 1f;
        dependRate = 0.7f;
        isBoss = true;
    }

    protected override void Start()
    {
        base.Start();
        HitboxLOff();
        HitboxROff();
    }

    public override void TakeDamage(int minDamage, int maxDamage)
    {
        base.TakeDamage(minDamage, maxDamage);
        Debug.Log("Golem Damaged");
    }

    public override void ExpUp()
    {
        base.ExpUp();
        playerstat.data.expCount += 100;
    }

    public override void EnemyDie() // In DeadState
    {
        if (spawner != null)
        {
            spawner.NotifyEnemyDead(gameObject);
        }
        else
        {
            DestroyEnemy();
        }
    }

    public override void DropItem()
    {
        foreach (GameObject dropItem in dropItems)
        {
            var rotation = Random.Range(0f, 180f);
            GameObject dropped = Instantiate(dropItem, transform.position, Quaternion.Euler(rotation, 0f, rotation));
            Rigidbody rb = dropped.GetComponent<Rigidbody>();
            if (rb != null)
            {
                var randomUp = 0.05f;
                var randomRight = Random.Range(-0.05f, 0.05f);
                rb.AddForce(Vector3.up * randomUp + Vector3.right * randomRight, ForceMode.Impulse);
            }
            Debug.Log("Item Dropped");
        }
    }

    public override void DropCoin()
    {
        for (int i = 0; i < 5; i++)
        {
            var rotation = Random.Range(0f, 180f);
            GameObject coindropped = Instantiate(dropCoin, transform.position, Quaternion.Euler(rotation, 0f, rotation));
            Rigidbody rb = coindropped.GetComponent<Rigidbody>();

            if (rb != null)
            {
                var randomUp = 0.05f;
                var randomRight = Random.Range(-0.05f, 0.05f);
                rb.AddForce(Vector3.up * randomUp + Vector3.right * randomRight, ForceMode.Impulse);
            }
            Debug.Log("Coin Dropped");
        }
    }

    public void HitboxLOn() => hitboxL.SetActive(true);
    public void HitboxLOff() => hitboxL.SetActive(false);
    public void HitboxROn() => hitboxR.SetActive(true);
    public void HitboxROff() => hitboxR.SetActive(false);
    public void GolemAttackSoundPlay() => SoundManager.Instance.Play2DSound("Golem Attack Sound");
}
