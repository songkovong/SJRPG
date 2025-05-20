using TMPro;
using UnityEngine;

public class PlayerHealthTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text healthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        healthText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = Mathf.FloorToInt(player.playerStat.currentHealth).ToString() + "/" + Mathf.FloorToInt(player.playerStat.maxHealth).ToString();
    }
}
