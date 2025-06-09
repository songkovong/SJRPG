using TMPro;
using UnityEngine;

public class PlayerExpTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text expText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        expText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        expText.text = Mathf.FloorToInt(player.playerStat.expCount).ToString() + "/" + Mathf.FloorToInt(player.playerStat.exp).ToString();
    }
}
