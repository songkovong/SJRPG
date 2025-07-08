using System.Collections;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public Coin coin;

    void Start()
    {
        StartCoroutine(DestroyTime());
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    public int CoinRange()
    {
        return (int)Random.Range(this.coin.minCoinValue, this.coin.maxCoinValue);
    }
}
