using TMPro;
using UnityEngine;

public class PlayerDependTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text dependText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        dependText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        dependText.text = player.playerStat.data.dependRate.ToString();
    }
}
