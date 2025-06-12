using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillUI : MonoBehaviour
{
    public Player player;
    public int code = 0;
    Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // gameObject.GetComponent<Image>().fillAmount = player.playerStat.skillCooltimeTimer / player.playerStat.skillCooltime;
        if (code == 1)
        {
            if (player.playerStat.spaceSkill.IsLevel0())
            {
                image.color = Color.gray;
            }
            else
            {
                image.color = Color.white;
            }
            image.fillAmount = player.playerStat.spaceSkill.timer / player.playerStat.spaceSkill.cooltime;
        }
        else if (code == 2)
        {
            if (player.playerStat.cSkill.IsLevel0())
            {
                image.color = Color.gray;
            }
            else
            {
                image.color = Color.white;
            }
            image.fillAmount = player.playerStat.cSkill.timer / player.playerStat.cSkill.cooltime;
        }

        else if (code == 3)
        {
            if (player.playerStat.rSkill.IsLevel0())
            {
                image.color = Color.gray;
            }
            else
            {
                image.color = Color.white;
            }
            image.fillAmount = player.playerStat.rSkill.timer / player.playerStat.rSkill.cooltime;
        }
    }
}
