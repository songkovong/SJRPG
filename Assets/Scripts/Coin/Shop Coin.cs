using TMPro;
using UnityEngine;

public class ShopCoin : MonoBehaviour
{
    Player player;
    public TMP_Text coinText;

    void Start()
    {
        if (player == null)
        {
            player = Player.instance;
        }
    }

    void Update()
    {
        coinText.text = player.inventory.coin.currentCoin.ToString();
    }
}
