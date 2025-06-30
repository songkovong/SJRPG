using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : Enemy, IDamageable
{
    [SerializeField] private List<GameObject> dropItems;
    [SerializeField] private GameObject dropCoin;
    protected override void Awake()
    {
        base.Awake();
        attackCooltime = 1f;
        detectRadius = 7f;
        detectAttackRadius = 1.5f;
        moveSpeed = 1f;
        rotationSpeed = 2000f;
        attackDamage = 2f;
        maxHealth = 10f;
        currentHealth = maxHealth;
        godmodeDuration = 1f;
        dependRate = 0.1f;
    }

    public override void TakeDamage(float getDamage)
    {
        base.TakeDamage(getDamage);
        Debug.Log("Slime Damaged");
    }

    public override void ExpUp()
    {
        base.ExpUp();
        playerstat.data.expCount += 10;
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
            float rate = Random.Range(0f, 1f);
            // Item Drop
            if (dropItem?.GetComponent<ItemPickUp>().item.itemDropRate >= rate)
            {
                GameObject dropped = Instantiate(dropItem, transform.position, Quaternion.identity);
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
            GameObject coindropped = Instantiate(dropCoin, transform.position, Quaternion.identity);
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
