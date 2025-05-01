using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyBaseState enemyCurrentState;
    void Start()
    {
        ChangeState(new EnemyIdleState(this));
    }

    void Update()
    {
        enemyCurrentState?.Update();
        HitValue(transform.position, 2f, "Player", 1f);
    }

    public void ChangeState(EnemyBaseState newstate)
    {
        enemyCurrentState?.Exit();
        enemyCurrentState = newstate;
        enemyCurrentState.Enter();
    }

    void HitValue(Vector3 hitPosition, float hitRadius, string hitLayer, float hitDamage)
    {
        Collider[] hits = Physics.OverlapSphere(hitPosition, hitRadius, LayerMask.GetMask(hitLayer));
        foreach (Collider hit in hits)
        {
            hit.GetComponent<PlayerStat>()?.TakeDamage(hitDamage);
            // hit.GetComponent<Player>().isHit = true;
            // hit.GetComponent<Player>().hitDamage = hitDamage;
        }
    }

    public void TakeDamage(float getDamage)
    {
        Debug.Log("Damage + " + getDamage);
    }
}
