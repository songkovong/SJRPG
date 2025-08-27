using System.Collections.Generic;
using UnityEngine;

public class Beholder : Enemy, IDamageable
{
    [SerializeField] private List<GameObject> dropItems;
    [SerializeField] private GameObject dropCoin;
    protected override void Awake()
    {
        base.Awake();
        thisEnemyCode = 6;
        attackCooltime = 0.7f;
        detectRadius = 13f;
        detectAttackRadius = 2f;
        moveSpeed = 3.5f;
        rotationSpeed = 2000f;
        attackDamage = 20f;
        maxHealth = 35f;
        currentHealth = maxHealth;
        godmodeDuration = 0.8f;
        dependRate = 0.1f;
    }

    public override void TakeDamage(int minDamage, int maxDamage)
    {
        base.TakeDamage(minDamage, maxDamage);
        Debug.Log("Beholder Damaged");
    }

    public override void ExpUp()
    {
        base.ExpUp();
        playerstat.data.expCount += 28;
    }

    public override void EnemyDie() // In DeadState
    {
        QuestManager.Instance.UpdateEnemyKill(thisEnemyCode);
        
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
            float rate = Random.Range(0f, 1f);
            // Item Drop
            if (dropItem?.GetComponent<ItemPickUp>().item.itemDropRate >= rate)
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
    }

    public override void DropCoin()
    {
        float rate = Random.Range(0f, 1f);
        if (dropCoin?.GetComponent<CoinPickUp>().coin.coinDropRate >= rate)
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
}
