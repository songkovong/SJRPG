using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : Enemy, IDamageable
{
    [SerializeField] private List<GameObject> dropItems;
    [SerializeField] private GameObject dropCoin;

    protected override void Awake()
    {
        base.Awake();
        thisEnemyCode = 1;
        attackCooltime = 1.5f;
        detectRadius = 7f;
        detectAttackRadius = 2.5f;
        moveSpeed = 2f;
        rotationSpeed = 2000f;
        attackDamage = 3f;
        maxHealth = 20f;
        currentHealth = maxHealth;
        godmodeDuration = 1f;
        dependRate = 0.5f;
    }

    public override void TakeDamage(float getDamage)
    {
        base.TakeDamage(getDamage);
        Debug.Log("TurtelShell Damaged");
    }

    public override void ExpUp()
    {
        base.ExpUp();
        playerstat.data.expCount += 20;
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

        // Item Drop
        Debug.Log("Item Dropped");
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
