using TMPro;
using UnityEngine;

public class PlayerMagicTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text magicText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        magicText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        magicText.text = Mathf.FloorToInt(player.playerStat.currentMagic).ToString() + "/" + Mathf.FloorToInt(player.playerStat.maxMagic).ToString();
    }
}
