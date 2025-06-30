using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Player player;
    public int code;
    string name;
    string desc;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.GetComponent<Player>();

        if (player == null)
        {
            // text.text = "Player Not Found";
            return;
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetSkillTooltip();
        TooltipController.instance?.ShowTooltip(name, desc, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipController.instance?.HideTooltip();
    }

    void GetSkillTooltip()
    {
        switch (code)
        {
            case 1:
                name = "Slash";
                desc = "Player Spin Attack " + player.playerStat.spaceSkill.damage * 100 + "% damage.";
                break;
            case 2:
                name = "Rage";
                desc = "Player attack damage is double and duration is " + player.playerStat.cSkill.duration + "seconds.";
                break;
            case 3:
                name = "Strike";
                desc = "Player Strike in Air " + player.playerStat.rSkill.damage * 100 + "% damage.";
                break;
            case 4:
                name = "Attack Mastery";
                desc = "Player Attack Mastery increase " + player.playerStat.attackMastery.masteryStat * 100 + "%.";
                break;
            case 5:
                name = "Guard";
                desc = "Player can guard to click mouse right button and mana consume " + player.playerStat.guardSkill.masteryStat + " in 1 second.";
                break;
            default:
                name = "No Skill";
                desc = "No Skill Information.";
                break;
        }
    }
}
