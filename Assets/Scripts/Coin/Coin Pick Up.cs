using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public Coin coin;

    public int CoinRange()
    {
        return (int)Random.Range(this.coin.minCoinValue, this.coin.maxCoinValue);
    }
}
