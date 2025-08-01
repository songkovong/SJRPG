using System.Collections;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public bool isDestroy = true;

    void Start()
    {
        if (isDestroy)
        {
            StartCoroutine(DestroyTime());
        }
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(60f);
        Destroy(gameObject);
    }
}
