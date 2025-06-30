using TMPro;
using UnityEngine;

public class PlayerCoin : MonoBehaviour
{
    [SerializeField]
    TMP_Text text_currentCoin;

    private int currentCoin = 0;

    void Start()
    {
        SetCoinCount();
    }

    public void AddCoin(int _amount)
    {
        currentCoin += _amount;
    }

    public void SetCoinCount()
    {
        text_currentCoin.text = currentCoin.ToString();
    }
}
