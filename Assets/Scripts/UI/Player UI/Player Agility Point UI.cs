using TMPro;
using UnityEngine;

public class PlayerAgilityPointUI : MonoBehaviour
{
    public Player player;
    TMP_Text agilityPointText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        agilityPointText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        agilityPointText.text = player.playerStat.agility.ToString();
    }
}
