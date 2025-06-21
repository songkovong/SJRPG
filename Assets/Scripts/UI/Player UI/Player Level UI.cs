using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUI : MonoBehaviour
{
    public Player player;
    TMP_Text levelText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        levelText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = player.playerStat.data.level.ToString();
    }
}
