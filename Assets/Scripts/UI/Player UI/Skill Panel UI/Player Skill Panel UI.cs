using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSkillPanelUI : MonoBehaviour
{
    Player player;
    TMP_Text text;
    public int code = 0;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        player = GameObject.FindWithTag("Player")?.GetComponent<Player>();

        if (player == null)
        {
            // text.text = "Player Not Found";
            return;
        }

        // UpdateSkill();
    }

    void Update()
    {
        UpdateSkill();
    }

    void UpdateSkill()
    {
        switch (code)
        {
            case 1:
                text.text = player.playerStat.spaceSkill.level.ToString();
                break;
            case 2:
                text.text = player.playerStat.cSkill.level.ToString();
                break;
            case 3:
                text.text = player.playerStat.rSkill.level.ToString();
                break;
            default:
                text.text = "Player Skill Not Found";
                break;
        }
    }
}
