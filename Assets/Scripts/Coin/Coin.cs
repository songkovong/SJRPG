using UnityEngine;

[CreateAssetMenu(fileName = "New Coin", menuName = "New Coin/Coin")]
public class Coin : ScriptableObject
{
    public enum CoinType
    {
        Gold,
        Sliver,
        Bronze
    }

    public string coinName;
    public CoinType coinType;
    public float minCoinValue;
    public float maxCoinValue;
    public float coinDropRate;
    public GameObject coinPrefab;
}
