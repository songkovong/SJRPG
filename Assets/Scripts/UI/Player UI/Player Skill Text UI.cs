using TMPro;
using UnityEngine;

public class PlayerSkillTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text skillText;
    float cooltime1;
    float cooltime2;
    float cooltime3;
    public int code = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        skillText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // cooltime = player.playerStat.skillCooltime - player.playerStat.skillCooltimeTimer;
        cooltime1 = player.playerStat.spaceSkill.cooltime - player.playerStat.spaceSkill.timer;
        cooltime2 = player.playerStat.cSkill.cooltime - player.playerStat.cSkill.timer;
        cooltime3 = player.playerStat.rSkill.cooltime - player.playerStat.rSkill.timer;
        if (code == 1)
        {
            if (cooltime1 == 0)
            {
                skillText.text = " ";
            }
            else
            {
                skillText.text = Mathf.FloorToInt(cooltime1).ToString();
            }
        }
        else if (code == 2)
        {
            if (cooltime2 == 0)
            {
                skillText.text = " ";
            }
            else
            {
                skillText.text = Mathf.FloorToInt(cooltime2).ToString();
            }
        }
        else if (code == 3)
        {
            if (cooltime3 == 0)
            {
                skillText.text = " ";
            }
            else
            {
                skillText.text = Mathf.FloorToInt(cooltime3).ToString();
            }
        }
    }
}
