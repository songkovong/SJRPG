using System.Collections;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    void Start()
    {
        StartCoroutine(DestroyTime());
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
