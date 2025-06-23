using TMPro;
using UnityEngine;

public class PlayerStrengthPointUI : MonoBehaviour
{
    public Player player;
    TMP_Text strengthPointText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        strengthPointText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        strengthPointText.text = player.playerStat.data.strength.ToString();
    }
}
