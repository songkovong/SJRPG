using TMPro;
using UnityEngine;

public class PlayerStatPointUI : MonoBehaviour
{
    public Player player;
    TMP_Text statPointText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        statPointText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        statPointText.text = player.playerStat.data.statPoint.ToString() + " SP";
    }
}
