using TMPro;
using UnityEngine;

public class PlayerSprintSpeedTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text sprintSpeedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        sprintSpeedText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        sprintSpeedText.text = player.playerStat.data.sprintSpeed.ToString();
    }
}
