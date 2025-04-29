using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 2f, LayerMask.GetMask("Player"));
        foreach (Collider hit in hits)
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(1f);
        }
    }
}
