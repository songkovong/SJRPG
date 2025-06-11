using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillUI : MonoBehaviour
{
    public Player player;
    public int code = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // gameObject.GetComponent<Image>().fillAmount = player.playerStat.skillCooltimeTimer / player.playerStat.skillCooltime;
        if (code == 1)
        {
            gameObject.GetComponent<Image>().fillAmount = player.playerStat.spaceSkill.timer / player.playerStat.spaceSkill.cooltime;
        }
        else if (code == 2)
        {
            gameObject.GetComponent<Image>().fillAmount = player.playerStat.cSkill.timer / player.playerStat.cSkill.cooltime;
        }
    }
}
