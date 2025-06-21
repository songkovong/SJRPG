using TMPro;
using UnityEngine;

public class PlayerSpeedTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text speedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        speedText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = player.playerStat.data.moveSpeed.ToString();
    }
}
