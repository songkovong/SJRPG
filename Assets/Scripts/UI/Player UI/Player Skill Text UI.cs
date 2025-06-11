using TMPro;
using UnityEngine;

public class PlayerSkillTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text skillText;
    float cooltime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        skillText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        cooltime = player.playerStat.skillCooltime - player.playerStat.skillCooltimeTimer;
        if (cooltime == 0)
        {
            skillText.text = " ";
        }
        else
        {
            skillText.text = Mathf.FloorToInt(cooltime).ToString();
        }
    }
}
