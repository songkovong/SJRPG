using TMPro;
using UnityEngine;

public class PlayerExpTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text skillText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        skillText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        skillText.text = Mathf.FloorToInt(player.playerStat.expCount).ToString() + "/" + Mathf.FloorToInt(player.playerStat.exp).ToString();
    }
}
